using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestGlow
{
    /// <summary>
    /// A thin light that runs round the edge of a quest box: a bright head, a tail that thins and
    /// fades behind it, and a soft glow on the outside only. Sits as an extra child of the box,
    /// stretched over it, ignored by layout and raycasts, so the quest itself is left alone.
    /// </summary>
    public class QuestBeam : MaskableGraphic
    {
        private const string ObjectName = "QuestGlowBeam";

        // distance between points along a fading strip, in canvas pixels
        private const float Step = 3f;
        private const float GlowStrength = 0.35f;
        // the tail thins to this share of the line width
        private const float TailWidth = 0.25f;
        // how far the head lightens towards white
        private const float Shine = 0.5f;
        // share of a lap each row runs behind the one above, so the list ripples instead of moving in lockstep
        private const float RowStagger = 0.07f;
        // the resting outline is drawn thinner than the beam
        private const float OutlineWidth = 0.6f;

        // How a strip looks at its start (t = 0) and its end (t = 1). Everything between is blended.
        private struct Look
        {
            public Color ColorFrom, ColorTo;
            public float AlphaFrom, AlphaTo;
            public float WidthFrom, WidthTo;
            public float Glow;
            public float Shine;

            public bool Varies => ColorFrom != ColorTo || AlphaFrom != AlphaTo || WidthFrom != WidthTo || Shine > 0f;
        }

        private Need _need;
        private float _offset;
        private bool _drawing;
        private readonly List<RectTransform> _clips = new List<RectTransform>(4);
        private bool _clipsFound;
        private readonly Color[] _colors = new Color[3];
        private readonly Vector3[] _corners = new Vector3[4];
        private readonly List<float> _samples = new List<float>(256);

        internal static void Attach(Transform box, Need need, int row)
        {
            if (box == null)
                return;

            Transform existing = box.Find(ObjectName);
            QuestBeam beam = existing != null ? existing.GetComponent<QuestBeam>() : null;

            // list items get reused for other quests, so a plain quest has to switch it back off
            if (need == Need.None)
            {
                if (beam != null)
                    beam.gameObject.SetActive(false);
                return;
            }

            if (beam == null)
                beam = Create(box);

            beam._need = need;
            beam._offset = -row * RowStagger;
            beam._clipsFound = false;
            beam.transform.SetAsLastSibling();
            beam.gameObject.SetActive(true);
            beam.SetVerticesDirty();
        }

        private static QuestBeam Create(Transform box)
        {
            var go = new GameObject(ObjectName, typeof(RectTransform));
            go.layer = box.gameObject.layer;
            go.AddComponent<LayoutElement>().ignoreLayout = true;

            var rt = (RectTransform)go.transform;
            rt.SetParent(box, false);
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var beam = go.AddComponent<QuestBeam>();
            beam.raycastTarget = false;
            return beam;
        }

        private void Update()
        {
            // scrolled out of the list, nothing to see
            if (canvasRenderer.cull)
                return;

            // redraw every frame while there is something to move, and once more after
            // the config turns it off so the last frame gets cleared
            if (_drawing || CollectColors() > 0)
                SetVerticesDirty();
        }

        private int CollectColors()
        {
            if (!Plugin.Enabled.Value)
                return 0;

            bool kappa = (_need & Need.Kappa) != 0 && Plugin.ShowKappa.Value;
            bool lightkeeper = (_need & Need.Lightkeeper) != 0 && Plugin.ShowLightkeeper.Value;

            if (kappa && lightkeeper)
            {
                if (Plugin.Both.Value == BothMode.GoldOnly)
                    lightkeeper = false;
                else if (Plugin.Both.Value == BothMode.BlueOnly)
                    kappa = false;
            }

            int n = 0;
            if (kappa)
                _colors[n++] = Plugin.KappaColor.Value;
            if (lightkeeper)
                _colors[n++] = Plugin.LightkeeperColor.Value;
            if ((_need & Need.Lockout) != 0 && Plugin.ShowLockout.Value)
                _colors[n++] = Plugin.LockoutColor.Value;
            return n;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            int count = CollectColors();
            _drawing = count > 0;

            float glow = Plugin.Glow.Value;
            Rect r = VisibleRect(glow);
            if (count == 0 || r.width < 2f || r.height < 2f)
                return;

            float perimeter = 2f * (r.width + r.height);
            float lap = Mathf.Max(0.1f, Plugin.LapSeconds.Value);
            float phase = Mathf.Repeat(Time.unscaledTime / lap + _offset, 1f);
            float resting = Plugin.OutlineOpacity.Value;
            float width = Plugin.Thickness.Value;
            // beams are spread evenly round the box, and a tail never runs into the beam behind it
            float gap = perimeter / count;
            float tail = Mathf.Min(Plugin.BeamLength.Value * perimeter, gap);

            // faint outline first so the beams sit on top of it. Between two beams it blends
            // from the colour in front to the colour behind.
            if (resting > 0f)
            {
                for (int i = 0; i < count; i++)
                {
                    float head = (phase + (float)i / count) * perimeter;
                    DrawStrip(vh, r, head - gap, head, new Look
                    {
                        ColorFrom = _colors[(i + count - 1) % count],
                        ColorTo = _colors[i],
                        AlphaFrom = resting,
                        AlphaTo = resting,
                        WidthFrom = width * OutlineWidth,
                        WidthTo = width * OutlineWidth
                    });
                }
            }

            for (int i = 0; i < count; i++)
            {
                float head = (phase + (float)i / count) * perimeter;
                DrawStrip(vh, r, head - tail, head, new Look
                {
                    ColorFrom = _colors[i],
                    ColorTo = _colors[i],
                    AlphaFrom = 0f,
                    AlphaTo = 1f,
                    WidthFrom = width * TailWidth,
                    WidthTo = width,
                    Glow = glow,
                    Shine = Shine
                });
            }
        }

        // The box, narrowed to what the list actually shows. On Character > Tasks the rows run
        // past the right edge of the scroll view, which cut off the right side of the outline.
        // Only the sides are fitted: a row scrolled half out at the top or bottom should look
        // cut off, not squashed.
        private Rect VisibleRect(float glow)
        {
            Rect r = rectTransform.rect;
            float left = float.NegativeInfinity, right = float.PositiveInfinity;

            foreach (RectTransform clip in Clips())
            {
                if (clip == null)
                    continue;
                clip.GetWorldCorners(_corners);
                left = Mathf.Max(left, rectTransform.InverseTransformPoint(_corners[0]).x);
                right = Mathf.Min(right, rectTransform.InverseTransformPoint(_corners[2]).x);
            }

            // leave room for the glow, but only where the box actually runs over the edge
            float room = Mathf.Max(glow, 1f);
            if (r.xMax > right + 0.5f)
                r.xMax = Mathf.Max(right - room, r.xMin);
            if (r.xMin < left - 0.5f)
                r.xMin = Mathf.Min(left + room, r.xMax);
            return r;
        }

        // Everything above the box that can cut it off: each mask (the task row's own mask can be
        // as wide as the row, so the first one found isn't enough), each scroll view's viewport
        // (the scrollbar sits beside it), and the screen.
        private List<RectTransform> Clips()
        {
            if (_clipsFound)
                return _clips;

            _clipsFound = true;
            _clips.Clear();
            for (Transform t = transform.parent; t != null; t = t.parent)
            {
                var rectMask = t.GetComponent<RectMask2D>();
                var mask = t.GetComponent<Mask>();
                if ((rectMask != null && rectMask.enabled) || (mask != null && mask.enabled))
                    _clips.Add(t as RectTransform);

                var scroll = t.GetComponent<ScrollRect>();
                if (scroll != null && scroll.viewport != null)
                    _clips.Add(scroll.viewport);
            }

            if (canvas != null)
                _clips.Add(canvas.rootCanvas.transform as RectTransform);
            return _clips;
        }

        // Draws the outline between two distances along the edge (clockwise from the top left
        // corner). Each point gets four vertices across the line: glow outer edge, box edge
        // (twice, glow and line colour) and the line's inner edge.
        private void DrawStrip(VertexHelper vh, Rect r, float from, float to, Look look)
        {
            float length = to - from;
            if (length < 0.5f)
                return;

            // a flat strip only needs its corners, a changing one needs points all along it
            BuildSamples(r, from, to, look.Varies);

            int start = vh.currentVertCount;

            for (int i = 0; i < _samples.Count; i++)
            {
                float d = _samples[i];
                float t = (d - from) / length;

                Color color = Color.Lerp(look.ColorFrom, look.ColorTo, t);
                float alpha = Mathf.Lerp(look.AlphaFrom, look.AlphaTo, t * t) * color.a;
                float width = Mathf.Lerp(look.WidthFrom, look.WidthTo, t);
                Color line = look.Shine > 0f ? Color.Lerp(color, Color.white, Mathf.Pow(t, 8f) * look.Shine) : color;

                Vector2 p = PointAt(r, d);
                vh.AddVert(p + Outward(r, p) * look.Glow, WithAlpha(color, 0f), Vector2.zero);
                vh.AddVert(p, WithAlpha(color, alpha * GlowStrength), Vector2.zero);
                vh.AddVert(p, WithAlpha(line, alpha), Vector2.zero);
                vh.AddVert(Inset(r, p, width), WithAlpha(line, alpha), Vector2.zero);
            }

            for (int i = 0; i < _samples.Count - 1; i++)
            {
                int a = start + i * 4;
                int b = a + 4;
                if (look.Glow > 0f)
                    Quad(vh, a, b, 0);
                Quad(vh, a, b, 2);
            }
        }

        private static void Quad(VertexHelper vh, int a, int b, int offset)
        {
            vh.AddTriangle(a + offset, a + offset + 1, b + offset + 1);
            vh.AddTriangle(b + offset + 1, b + offset, a + offset);
        }

        // Points along the strip every Step pixels, plus every corner it passes, so corners stay sharp.
        private void BuildSamples(Rect r, float from, float to, bool dense)
        {
            _samples.Clear();

            float step = dense ? Step : to - from;
            for (float d = from; d < to; d += step)
                _samples.Add(d);
            _samples.Add(to);

            float w = r.width, h = r.height, perimeter = 2f * (w + h);
            for (float lapStart = Mathf.Floor(from / perimeter) * perimeter; lapStart < to; lapStart += perimeter)
            {
                AddCorner(lapStart, from, to);
                AddCorner(lapStart + w, from, to);
                AddCorner(lapStart + w + h, from, to);
                AddCorner(lapStart + w + h + w, from, to);
            }

            _samples.Sort();
        }

        private void AddCorner(float d, float from, float to)
        {
            if (d > from && d < to)
                _samples.Add(d);
        }

        private static Vector2 PointAt(Rect r, float d)
        {
            float w = r.width, h = r.height;
            d = Mathf.Repeat(d, 2f * (w + h));

            if (d < w)
                return new Vector2(r.xMin + d, r.yMax);
            d -= w;
            if (d < h)
                return new Vector2(r.xMax, r.yMax - d);
            d -= h;
            if (d < w)
                return new Vector2(r.xMax - d, r.yMin);
            d -= w;
            return new Vector2(r.xMin, r.yMin + d);
        }

        // Pulls a point on the edge into the box. Clamping into a smaller rect gives
        // mitred corners for free.
        private static Vector2 Inset(Rect r, Vector2 p, float by)
        {
            float x = Mathf.Min(by, r.width * 0.5f);
            float y = Mathf.Min(by, r.height * 0.5f);
            return new Vector2(
                Mathf.Clamp(p.x, r.xMin + x, r.xMax - x),
                Mathf.Clamp(p.y, r.yMin + y, r.yMax - y));
        }

        // Straight out from the edge. Normalised so the glow is cut off at the corners
        // instead of poking out square.
        private static Vector2 Outward(Rect r, Vector2 p)
        {
            const float eps = 0.01f;
            float x = p.x <= r.xMin + eps ? -1f : p.x >= r.xMax - eps ? 1f : 0f;
            float y = p.y <= r.yMin + eps ? -1f : p.y >= r.yMax - eps ? 1f : 0f;
            return new Vector2(x, y).normalized;
        }

        private static Color32 WithAlpha(Color c, float alpha)
        {
            c.a = Mathf.Clamp01(alpha);
            return c;
        }
    }
}
