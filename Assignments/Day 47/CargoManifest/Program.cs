namespace CargoManifest
{
        class Item
        {
            public string Name { get; set; }
            public double Weight { get; set; }
            public string Category { get; set; }
            public Item(string name, double weight, string category)
            {
                Name = name;
                Weight = weight;
                Category = category;
            }
    }

        class Container
        {
            public string ContainerID { get; set; }
            public List<Item> Items { get; set; }
            public Container(string id, List<Item> items)
            {
                ContainerID = id;
                Items = items;
            }
        }

        internal class Program
        {
            static public List<string> FindHeavyContainers(List<List<Container>> cargoBay, double threshWei)
            {
                List<string> result = new List<string>();

                if (cargoBay == null)
                    return result;

                foreach (var row in cargoBay)
                {
                    if (row == null) continue;

                    foreach (var con in row)
                    {
                        if (con == null || con.Items == null)
                            continue;

                        double totalW = 0;

                        foreach (var item in con.Items)
                        {
                            if (item != null)
                                totalW += item.Weight;
                        }

                        if (totalW > threshWei)
                            result.Add(con.ContainerID);
                    }
                }

                return result;
            }
        static Dictionary<string, int> GetItemCountsByCategory(List<List<Container>> cargoBay)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            if(cargoBay == null) return res;
            foreach(var row in cargoBay)
            {
                if(row == null) continue;
                foreach(var con in row)
                {
                    if (con == null || con.Items == null) continue;
                    foreach(var it in con.Items)
                    {
                        if(it== null) continue;
                        if (!res.ContainsKey(it.Category))
                            res[it.Category] = 0;

                        res[it.Category]++;
                    }
                    
                }
            }
            return res;
        }
          
        static public List<Item> FlattenAndSortShipment(List<List<Container>> cargoBay)
        {
            if (cargoBay == null)
                return new List<Item>();
            var newList = cargoBay.Where(r=>r!=null)
                                  .SelectMany(r=>r)
                                  .Where(con=>con!=null && con.Items != null)
                                  .SelectMany(con=>con.Items)
                                  .Where(it=>it!=null)
                                  .OrderBy(it=>it.Category)
                                  .ThenByDescending(it=>it.Weight)
                                  .ToList();
            return newList;
        }
            static void Main(string[] args)
            {
            var cargoBay = new List<List<Container>>
            {
                // ROW 0: High-Value Tech Row
                new List<Container>
                {
                    new Container("C001", new List<Item>
                    {
                        new Item("Laptop", 2.5, "Tech"),
                        new Item("Monitor", 5.0, "Tech"),
                        new Item("Smartphone", 0.5, "Tech")
                    }),
                    new Container("C104", new List<Item>
                    {
                        new Item("Server Rack", 45.0, "Tech"), // Heavy Item
                        new Item("Cables", 1.2, "Tech")
                    })
                },

                // ROW 1: Mixed Consumer Goods
                new List<Container>
                {
                    new Container("C002", new List<Item>
                    {
                        new Item("Apple", 0.2, "Food"),
                        new Item("Banana", 0.2, "Food"),
                        new Item("Milk", 1.0, "Food")
                    }),
                    new Container("C003", new List<Item>
                    {
                        new Item("Table", 15.0, "Furniture"),
                        new Item("Chair", 7.5, "Furniture")
                    })
                },

                // ROW 2: Fragile & Perishables (Includes an Empty Container)
                new List<Container>
                {
                    new Container("C205", new List<Item>
                    {
                        new Item("Vase", 3.0, "Decor"),
                        new Item("Mirror", 12.0, "Decor")
                    }),
                    new Container("C206", new List<Item>()) // EDGE CASE: Container with no items
                },

                // ROW 3: EDGE CASE - Empty Row
                new List<Container>() // A row that exists but has no containers
            };
            Console.WriteLine("heavy containers");
            var hC = FindHeavyContainers(cargoBay, 20);
            foreach(var i in hC)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("count");
            var cou = GetItemCountsByCategory(cargoBay);
            foreach (var k in cou)
            {
                Console.WriteLine($"{k.Key} {k.Value}");
            }
            Console.WriteLine("flattened");
            var flat = FlattenAndSortShipment(cargoBay);
            foreach (var f in flat)
            {
                Console.WriteLine($"{f.Category} {f.Name} {f.Weight}");
            }
        }
        }
    }
