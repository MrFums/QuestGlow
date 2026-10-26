namespace QuestGlow
{
    // The built-in list, used until Quest Glow's server part answers, or when it isn't installed.
    // Worked out from SPT 4.1's quest data the same way the server part does it: every quest on the
    // way to the Collector (Kappa) and to Knock-Knock (Lightkeeper), then the quests that lock each
    // other out. K = Kappa, L = Lightkeeper, X = lock-out. Grouped by the trader who gives the quest.
    // A quest can be in both parts, its flags are added together.
    internal static partial class QuestList
    {
        private static readonly (string Id, Need Need)[] Entries =
        {
            // Prapor
            ("5eda19f0edce541157209cee", K),     // Anesthesia
            ("5936da9e86f7742d65037edf", K | L), // Background Check
            ("5967530a86f77462ba22226b", K | L), // Bad Rep Evidence
            ("675c3507a06634b5110e3c18", K),     // Belka and Strelka
            ("597a171586f77405ba6887d3", K | L), // Big Customer
            ("59c124d686f774189b3c843f", K | L), // BP Depot
            ("5936d90786f7742b1420ba5b", K | L), // Debut
            ("59674eb386f774539f14813a", K | L), // Delivery From the Past
            ("60896b7bfa70fc097863b8f5", K),     // Documents
            ("6179ac7511973d018217d0b9", K),     // Easy Job - Part 1
            ("6179acbdc760af5ad2053585", K),     // Easy Job - Part 2
            ("5c0d190cd09282029f5390d8", K),     // Grenadier
            ("59675d6c86f7740a842fc482", K | L), // Ice Cream Cones
            ("60e71bb4e456d449cd47ca75", K),     // Intimidator
            ("657315e1dccd301f1301416a", K | L), // Luxurious Life
            ("60896bca6ee58f38c417d4f2", K),     // No Place for Renegades
            ("5c12452c86f7744b83469073", K),     // Perfect Mediator
            ("5979f8bb86f7743ec214c7a6", K),     // Polikhim Hobo
            ("669fa399033a3ce9870338a8", K),     // Possessor
            ("59675ea386f77414b32bded2", K),     // Postman Pat - Part 1
            ("626bd75c71bd851e971b82a5", K),     // Reconnaissance
            ("5d4bec3486f7743cac246665", K),     // Regulated Materials
            ("5fd9fad9c1ce6b1a3b486d00", K),     // Search Mission
            ("5967725e86f774601a446662", K | L), // Shaking Up the Teller
            ("657315df034d76585f032e01", K | L), // Shooting Cans
            ("5c0bd94186f7747a727f09b2", K),     // Test Drive - Part 1
            ("63a5cf262964a7488f5243ce", K),     // Test Drive - Part 2
            ("64f5deac39e45b527a7c4232", K),     // Test Drive - Part 3
            ("6574e0dedc0d635f633a5805", K),     // Test Drive - Part 4
            ("669fa3a40c828825de06d6a1", K),     // Test Drive - Part 5
            ("675c1ff1a757ddd00404f0aa", K),     // Test Drive - Part 6
            ("5ede55112c95834b583f052a", K),     // The Bunker - Part 1
            ("5ede567cfa6dc072ce15d6e3", K),     // The Bunker - Part 2
            ("59c50a9e86f7745fef66f4ff", K | L), // The Punisher - Part 1
            ("59c50c8886f7745fed3193bf", K | L), // The Punisher - Part 2
            ("59c512ad86f7741f0d09de9b", K | L), // The Punisher - Part 3
            ("59ca264786f77445a80ed044", K | L), // The Punisher - Part 4
            ("59ca29fb86f77445ab465c87", K),     // The Punisher - Part 5
            ("59ca2eb686f77445a80ed049", K),     // The Punisher - Part 6

            // Therapist
            ("669fa3910c828825de06d69f", K),     // A Healthy Alternative
            ("675c03d1f7da9792a405549a", K),     // Abandoned Cargo
            ("669fa39ee749756c920d02c8", K),     // All Is Revealed
            ("5c0d0d5086f774363760aef2", K),     // Athlete
            ("596a218586f77420d232807c", K),     // Car Repair
            ("675c04f4db8807b75d0f38e8", K),     // Closer to the People
            ("5edab736cc183c769d778bc2", K),     // Colleagues - Part 1
            ("5edaba7c0c502106f869bc02", K),     // Colleagues - Part 2
            ("5edac34d0bb72a50635c2bfa", K),     // Colleagues - Part 3
            ("60e71c48c1bfa3050473b8e5", K),     // Crisis
            ("5c0d1c4cd0928202a02a6f5c", K),     // Decontamination Service
            ("60896e28e4a85c72ef3fa301", K | L), // Disease History
            ("626bd75b05f287031503c7f6", K),     // Drug Trafficking
            ("657315ddab5a49b71f098853", K | L), // First in Line
            ("596a1e6c86f7741ddc2d3206", K),     // General Wares
            ("5a68661a86f774500f48afb0", K | L), // Health Care Privacy - Part 1
            ("5a68663e86f774501078f78a", K | L), // Health Care Privacy - Part 2
            ("5a68665c86f774255929b4c7", K),     // Health Care Privacy - Part 3
            ("5a68667486f7742607157d28", K),     // Health Care Privacy - Part 4
            ("5a68669a86f774255929b4d4", K),     // Health Care Privacy - Part 5
            ("669fa3a3ad7f1eac2607ed48", K),     // Health Care Privacy - Part 6
            ("6179afd0bca27a099552e040", K | L), // Lost Contact
            ("597a160786f77477531d39d2", K | L), // Out of Curiosity
            ("5969f90786f77420d2328015", K | L), // Painkiller
            ("5969f9e986f7741dde183a50", K | L), // Pharmacist
            ("5c0be5fc86f774467a116593", K),     // Private Clinic
            ("59689ee586f7740d1570bbd5", K | L), // Sanitary Standards - Part 1
            ("596a204686f774576d4c95de", K | L), // Sanitary Standards - Part 2
            ("6179ad56c760af5ad2053587", K | L), // Seaside Vacation
            ("675c047fa46173572a0bd878", K),     // Shipment Tracking
            ("5967733e86f774602332fc84", K | L), // Shortage
            ("596a0e1686f7741ddf17dbee", K),     // Supply Plans

            // Skier
            ("657315e270bb0b8dba00cc48", K | L), // Burning Rubber
            ("5979f9ba86f7740f6c3fe9f2", K | L), // Chemical - Part 1
            ("597a0b2986f77426d66c0633", K | L), // Chemical - Part 2
            ("597a0e5786f77426d66c0636", K | L), // Chemical - Part 3
            ("597a0f5686f774273b74f676", K | L), // Chemical - Part 4
            ("5b4795fb86f7745876267770", K | L), // Chumming
            ("639dbaf17c898a131e1cffff", L),     // Debtor
            ("669fa395c4c5c04798002497", K),     // Exit Here
            ("5c0bdb5286f774166e38eed4", K),     // Flint
            ("5a27c99a86f7747d2c6bdd8e", K | L), // Friend From the West - Part 1
            ("5a27d2af86f7744e1115b323", K | L), // Friend From the West - Part 2
            ("5979eee086f774311955e614", K | L), // Golden Swag
            ("639135c3744e452011470807", L),     // House Arrest - Part 1
            ("5b47926a86f7747ccc057c15", K | L), // Informed Means Armed
            ("596a101f86f7741ddb481582", K),     // Kind of Sabotage
            ("5b4794cb86f774598100d5d4", K),     // Lend-Lease - Part 1
            ("6193850f60b34236ee0483de", K),     // Long Road
            ("6179b4f16e9dd54ac275e407", K),     // Missing Cargo
            ("675c3582f6ddc329a90f9c6d", K),     // Private Club
            ("5edabd13218d181e29451442", K),     // Rigged Game
            ("6089743983426423753cd58a", K),     // Safe Corridor
            ("5c1234c286f77406fa13baeb", K | L), // Setup
            ("596b455186f77457cb50eccb", K),     // Stirrup
            ("596b36c586f77450d6045ad2", K | L), // Supplier
            ("596b43fb86f77457ca186186", K | L), // The Extortionist
            ("669fa39c64ea11e84c0642a6", K),     // The Walls Have Eyes
            ("5b478eca86f7744642012254", K),     // Vitamins - Part 1
            ("5b478ff486f7744d184ecbbf", K),     // Vitamins - Part 2
            ("5979ed3886f77431307dc512", K | L), // Whats on the Flash Drive

            // Peacekeeper
            ("5a27bb1e86f7741f27621b7e", K | L), // Cargo X - Part 1
            ("5a27bb3d86f77411ea361a21", K | L), // Cargo X - Part 2
            ("5a27bb5986f7741dfb660900", K | L), // Cargo X - Part 3
            ("61958c366726521dd96828ec", K | L), // Cargo X - Part 4
            ("60896888e4a85c72ef3fa300", K),     // Classified Technologies
            ("5a27b80086f774429a5d7e20", K | L), // Eagle Eye
            ("5a27b75b86f7742e97191958", K | L), // Fishing Gear
            ("5a27b87686f77460de0252a8", K | L), // Humanitarian Supplies
            ("5c0bd01e86f7747cdd799e56", K),     // Insomnia
            ("5c0d0f1886f77457b8210226", K),     // Lend-Lease - Part 2
            ("669fa38fad7f1eac2607ed46", K),     // One Less Loose End
            ("6179aff8f57fb279792c60a1", K),     // Overpopulation
            ("5c0d4c12d09282029f539173", K),     // Peacekeeping Mission
            ("6179b4d1bca27a099552e04e", K),     // Revision - Lighthouse
            ("6086c852c945025d41566124", K),     // Revision - Reserve
            ("5edac020218d181e29451446", K),     // Samples
            ("5a27b7d686f77460d847e6a6", K | L), // Scrap Metal
            ("5a03153686f77442d90e2171", K | L), // Spa Tour - Part 1
            ("5a03173786f77451cb427172", K | L), // Spa Tour - Part 2
            ("5a0327ba86f77456b9154236", K | L), // Spa Tour - Part 3
            ("5a03296886f774569778596a", K | L), // Spa Tour - Part 4
            ("5a0449d586f77474e66227b7", K | L), // Spa Tour - Part 5
            ("5a27ba9586f7741b543d8e85", K | L), // Spa Tour - Part 6
            ("5a27bafb86f7741c73584017", K | L), // Spa Tour - Part 7
            ("5edac63b930f5454f51e128b", K),     // TerraGroup Employee
            ("5a27b9de86f77464e5044585", K | L), // The Cult - Part 1
            ("5a27ba1c86f77461ea5a3c56", K | L), // The Cult - Part 2
            ("5c0d4e61d09282029f53920e", K),     // The Guide
            ("5a27b7a786f774579c3eb376", K | L), // Tigr Safari
            ("5a27bb8386f7741c770d2d0a", K),     // Wet Job - Part 1
            ("5a27bbf886f774333a418eeb", K),     // Wet Job - Part 2
            ("5a27bc1586f7741f6d40fa2f", K),     // Wet Job - Part 3
            ("5a27bc3686f7741c73584026", K),     // Wet Job - Part 4
            ("5a27bc6986f7741c7358402b", K),     // Wet Job - Part 5
            ("5a27bc8586f7741b543d8ea4", K),     // Wet Job - Part 6

            // Mechanic
            ("5c0bde0986f77479cf22c2f8", K),     // A Shooter Born in Heaven
            ("625d6ffcaa168e51321d69d7", L),     // Assessment - Part 1
            ("625d6fff4149f1149b5b12c9", L),     // Assessment - Part 2
            ("625d7001c4874104f230c0c5", L),     // Assessment - Part 3
            ("6089736efa70fc097863b8f6", K),     // Back Door
            ("5ac3475486f7741d6224abd3", K),     // Bad Habit
            ("669fa39b91b0a8c9680fc467", K),     // Black Swan
            ("626bd75e47ea7f506e5493c5", K | L), // Broadcast - Part 1
            ("63913715f8e5dd32bf4e3aaa", L),     // Broadcast - Part 2
            ("669fa3a1c26f13bd04030f37", K),     // Capacity Check
            ("5f04886a3937dc337a6b8238", K),     // Chemistry Closet
            ("6179b3bdc7560e13d23eeb8d", K),     // Corporate Secrets
            ("6179b3a12153c15e937d52bc", K),     // Energy Crisis
            ("5ac345dc86f774288030817f", K | L), // Farming - Part 1
            ("5ac3460c86f7742880308185", K | L), // Farming - Part 2
            ("5ac3462b86f7741d6118b983", K),     // Farming - Part 3
            ("5ac3464c86f7741d651d6877", K),     // Farming - Part 4
            ("5c1128e386f7746565181106", K),     // Fertilizers
            ("669fa394e0c9f9fafa082897", K),     // Forklift Certified
            ("5ac23c6186f7741247042bad", K | L), // Gunsmith - Part 1
            ("5ae327c886f7745c7b3f2f3f", K | L), // Gunsmith - Part 10
            ("639872fc93ae507d5858c3a6", K),     // Gunsmith - Part 11
            ("5b47799d86f7746c5d6a5fd8", K),     // Gunsmith - Part 12
            ("5ac244c486f77413e12cf945", K),     // Gunsmith - Part 13
            ("639872fe8871e1272b10ccf6", K),     // Gunsmith - Part 14
            ("5ae3280386f7742a41359364", K),     // Gunsmith - Part 15
            ("5ac242ab86f77412464f68b4", K),     // Gunsmith - Part 16
            ("5b47749f86f7746c5d6a5fd4", K),     // Gunsmith - Part 17
            ("5b477b6f86f7747290681823", K),     // Gunsmith - Part 18
            ("639873003693c63d86328f25", K),     // Gunsmith - Part 19
            ("5ac2426c86f774138762edfe", K | L), // Gunsmith - Part 2
            ("5b477f7686f7744d1b23c4d2", K),     // Gunsmith - Part 20
            ("63987301e11ec11ff5504036", K),     // Gunsmith - Part 21
            ("5b47825886f77468074618d3", K),     // Gunsmith - Part 22
            ("5ac2428686f77412450b42bf", K | L), // Gunsmith - Part 3
            ("639872f9decada40426d3447", K | L), // Gunsmith - Part 4
            ("5ae3267986f7742a413592fe", K | L), // Gunsmith - Part 5
            ("5ae3270f86f77445ba41d4dd", K | L), // Gunsmith - Part 6
            ("5ac244eb86f7741356335af1", K | L), // Gunsmith - Part 7
            ("5ae3277186f7745973054106", K | L), // Gunsmith - Part 8
            ("639872fa9b4fb827b200d8e5", K | L), // Gunsmith - Part 9
            ("5c139eb686f7747878361a6f", K),     // Import
            ("5ac3479086f7742880308199", K),     // Insider
            ("5d2495a886f77425cd51e403", K | L), // Introduction
            ("625d70031ed3bb5bcc5bd9e5", L),     // Key to the Tower
            ("625d7005a4eb80027c4f2e09", L),     // Knock-Knock
            ("625d6ff5ddc94657c21a1625", L),     // Network Provider - Part 1
            ("625d6ffaf7308432be1d44c5", L),     // Network Provider - Part 2
            ("675c1570526ff496850895d9", K),     // Passion for Ergonomics
            ("5c0be13186f7746f016734aa", K),     // Psycho Sniper
            ("657315e4a6af4ab4b50f3459", K | L), // Saving the Mole
            ("5ac3477486f7741d651d6885", K),     // Scout
            ("5ac3467986f7741d6224abc2", K),     // Signal - Part 1
            ("5ac346a886f7744e1b083d67", K),     // Signal - Part 2
            ("5ac346cf86f7741d63233a02", K),     // Signal - Part 3
            ("5ac346e886f7741d6118b99b", K),     // Signal - Part 4
            ("6089732b59b92115597ad789", K),     // Surplus Goods

            // Ragman
            ("608974d01a66564e74191fc0", K | L), // A Fuel Matter
            ("5ae448e586f7744dcf0c2a67", K | L), // Big Sale
            ("675c085d59b0575973005f52", K),     // Break the Deal
            ("5ae4499a86f77449783815db", K),     // Charisma Brings Success
            ("5ae4493486f7744efa289417", K | L), // Database - Part 1
            ("5ae4493d86f7744b8e15aa8f", K | L), // Database - Part 2
            ("5ae4490786f7744ca822adcc", K),     // Dressed to Kill
            ("5ae449b386f77446d8741719", K),     // Gratitude
            ("5b478b1886f7744d1b23c57d", K),     // Hot Delivery
            ("608974af4b05530f55550c21", K),     // Inventory Check
            ("5b47891f86f7744d1b23c571", K),     // Living High is Not a Crime - Part 1
            ("5c1141f386f77430ff393792", K),     // Living High is Not a Crime - Part 2
            ("5ae448bf86f7744d733e55ee", K | L), // Make ULTRA Great Again
            ("5b478d0f86f7744d190d91b5", K),     // Minibus
            ("5ae449a586f7744bde357696", K),     // No Fuss Needed
            ("5ae448a386f7744d3730fff0", K | L), // Only Business
            ("5ae449c386f7744bde357697", K),     // Sales Night
            ("5c112d7e86f7740d6f647486", K),     // Scavenger
            ("5ae4495086f77443c122bc40", K),     // Sew it Good - Part 1
            ("5ae4495c86f7744e87761355", K),     // Sew it Good - Part 2
            ("5ae4496986f774459e77beb6", K),     // Sew it Good - Part 3
            ("5ae4497b86f7744cf402ed00", K),     // Sew it Good - Part 4
            ("5ae449d986f774453a54a7e1", K),     // Supervisor
            ("5ae448f286f77448d73c0131", K | L), // The Blood of War - Part 1
            ("5b47876e86f7744d1c353205", K),     // The Blood of War - Part 2
            ("5c10f94386f774227172c572", K),     // The Blood of War - Part 3
            ("5ae4498786f7744bde357695", K),     // The Key to Success

            // Jaeger
            ("5d24b81486f77439c92d6ba8", K | L), // Acquaintance
            ("5d25e46e86f77409453bce7c", K),     // Ambulance
            ("669fa3979b0ce3feae01a130", K),     // Claustrophobia
            ("5d25e48186f77443e625e386", K | L), // Courtesy Visit
            ("669fa3a08b4a64b332041ff7", K),     // Dragnet
            ("66b38e144f2ab7cc530c3fe7", K),     // Every Hunter Knows This
            ("5d25e4b786f77408251c4bfc", K),     // Fishing Place
            ("5d25e4ca86f77409dd5cdf2c", K),     // Hunting Trip
            ("5d25e4ad86f77443e625e387", K),     // Nostalgia
            ("608a768d82e40b3c727fd17d", K),     // Pest Control
            ("5d25e4d586f77443e625e388", K),     // Reserve
            ("675c1ec7a46173572a0bf20a", K),     // Rite of Passage
            ("66b38c7bf85b8bf7250f9cb6", K),     // Rough Tarkov
            ("5d25e48d86f77408251c4bfb", K),     // Shady Business
            ("626bdcc3a371ee3a7a3514c5", K),     // Stray Dogs
            ("63a88045abf76d719f42d715", K),     // The Delicious Sausage
            ("61904daa7d0d857927447b9c", K),     // The Hermit
            ("5d25e2d886f77442734d335e", K),     // The Huntsman Path - Controller
            ("5d25e44f86f77443e625e385", K),     // The Huntsman Path - Eraser - Part 1
            ("5d25e45e86f77408251c4bfa", K),     // The Huntsman Path - Eraser - Part 2
            ("5d25e44386f77409453bce7b", K),     // The Huntsman Path - Evil Watchman
            ("60c0c018f7afb4354815096a", K),     // The Huntsman Path - Factory Chief
            ("5d25e2cc86f77443e47ae019", K | L), // The Huntsman Path - Forest Cleaning
            ("5d25e43786f7740a212217fa", K),     // The Huntsman Path - Justice
            ("6179ad0a6e9dd54ac275e3f2", K),     // The Huntsman Path - Outcasts
            ("5edab4b1218d181e29451435", K),     // The Huntsman Path - Sadist
            ("5d25e2b486f77409de05bba0", K | L), // The Huntsman Path - Secured Perimeter
            ("5d25e2e286f77444001e2e48", K),     // The Huntsman Path - Sellout
            ("5d25e2c386f77443e7549029", K),     // The Huntsman Path - Trophy
            ("5d25e2ee86f77443e35162ea", K),     // The Huntsman Path - Woods Keeper
            ("5d25e2a986f77409dd5cdf2a", K),     // The Survivalist Path - Combat Medic
            ("5d25e29d86f7740a22516326", K),     // The Survivalist Path - Eagle-Owl
            ("5eaaaa7c93afa0558f3b5a1c", K),     // The Survivalist Path - Junkie
            ("5d25b6be86f77444001e1b89", K | L), // The Survivalist Path - Thrifty
            ("5d25cf2686f77443e75488d4", K | L), // The Survivalist Path - Tough Guy
            ("5d25aed386f77442734d25d2", K | L), // The Survivalist Path - Unprotected but Dangerous
            ("5d25c81b86f77443e625dd71", K | L), // The Survivalist Path - Wounded Beast
            ("5d25bfd086f77442734d3007", K | L), // The Survivalist Path - Zhivchik
            ("5bc4776586f774512d07cf05", K | L), // The Tarkov Shooter - Part 1
            ("5bc479e586f7747f376c7da3", K | L), // The Tarkov Shooter - Part 2
            ("5bc47dbf86f7741ee74e93b9", K | L), // The Tarkov Shooter - Part 3
            ("5bc480a686f7741af0342e29", K),     // The Tarkov Shooter - Part 4
            ("5bc4826c86f774106d22d88b", K),     // The Tarkov Shooter - Part 5
            ("5bc4836986f7740c0152911c", K),     // The Tarkov Shooter - Part 6
            ("5bc4856986f77454c317bea7", K),     // The Tarkov Shooter - Part 7
            ("5bc4893c86f774626f5ebf3e", K),     // The Tarkov Shooter - Part 8
            ("675c1cf4a757ddd00404f0a3", K),     // Work Smarter

            // Fence
            ("5c51aac186f77432ea65c552", K),     // Collector

            // Lock-outs: completing one quest of a group fails the others for good
            // (the Fail conditions in SPT's quests.json). Rivals in brackets.

            // Prapor
            ("597a171586f77405ba6887d3", X),     // Big Customer (Chemical - Part 4, Out of Curiosity)

            // Therapist
            ("669fa3910c828825de06d69f", X),     // A Healthy Alternative (One Less Loose End)
            ("5edac34d0bb72a50635c2bfa", X),     // Colleagues - Part 3 (The Huntsman Path - Sadist)
            ("597a160786f77477531d39d2", X),     // Out of Curiosity (Big Customer, Chemical - Part 4)
            ("67040cae4ac6d9c18c0ade2c", X),     // Radical Treatment (Forgotten Oaths)
            ("596a0e1686f7741ddf17dbee", X),     // Supply Plans (Kind of Sabotage)

            // Skier
            ("597a0f5686f774273b74f676", X),     // Chemical - Part 4 (Big Customer, Out of Curiosity)
            ("67460662d0fbbc74ca0f7229", X),     // Choose Your Friends Wisely (The Price of Independence)
            ("674602307e3818d5bb069489", X),     // Hindsight 2020 (The Price of Independence)
            ("6746053b5b555b53460d9896", X),     // Key Partner (The Price of Independence)
            ("674605df60a98cad1b0ec799", X),     // Killer Argument (The Price of Independence)
            ("596a101f86f7741ddb481582", X),     // Kind of Sabotage (Supply Plans)
            ("674600a366e6a521aa05eb66", X),     // Route Deviation (The Price of Independence)
            ("67f3eab9a33cd296b20ee695", X),     // Staff Shortage (Fearless Beast)
            ("6745fae369a58fceba10343d", X),     // The Higher They Fly (The Price of Independence)

            // Peacekeeper
            ("669fa38fad7f1eac2607ed46", X),     // One Less Loose End (A Healthy Alternative)

            // Mechanic
            ("6894bc838bbdab4ab109929c", X),     // Friend from Norvinsk - Alternative Solution (Friend from Norvinsk - Part 4)
            ("686403eeb4aaef121c0f0f06", X),     // Friend from Norvinsk - Part 4 (Friend from Norvinsk - Alternative Solution)

            // Jaeger
            ("67f3eacef649e7bceb0bb455", X),     // Fearless Beast (Staff Shortage)
            ("67040ccdcc1f3752720376ef", X),     // Forgotten Oaths (Radical Treatment)
            ("5edab4b1218d181e29451435", X),     // The Huntsman Path - Sadist (Colleagues - Part 3)

            // Fence
            ("683425dd8f5b18d29a05d9d1", X),     // Between Two Fires PVE ZONE (Decisions Decisions, Surprise Gift)

            // Ref
            ("6834254f2f0e2a7eb90b62ef", X),     // Decisions Decisions PVE ZONE (Between Two Fires, Surprise Gift)

            // Lightkeeper
            ("673f61a066e6a521aa04b62b", X),     // Order From Outside (Battery Change)
            ("6745fdddd3346c216702e0bf", X),     // Simple Side Job (Protect the Sky)
            ("683427418f5b18d29a05d9e3", X),     // Surprise Gift PVE ZONE (Between Two Fires, Decisions Decisions)

            // BTR Driver
            ("6744a9dfef61d56e020b5c4a", X),     // Battery Change (Order From Outside)
            ("6744ab1def61d56e020b5c56", X),     // Protect the Sky (Simple Side Job)
            ("6744af0969a58fceba101fed", X),     // The Price of Independence (Choose Your Friends Wisely, Hindsight 2020, Key Partner, Killer Argument, Route Deviation, The Higher They Fly)
            ("6745cbee909d2013670a4a55", X),     // The Price of Independence, second version (same rivals)
        };
    }
}
