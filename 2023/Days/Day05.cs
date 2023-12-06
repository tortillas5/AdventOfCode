using AdventOfCode.Utils;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 5 de advent of code.
    /// https://adventofcode.com/2023/day/5
    /// </summary>
    internal class Day05
    {
        /// <summary>
        /// Chemin vers l'input du jour 5.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day05.txt");

        /// <summary>
        /// Pour un Almanach, retourne l'emplacement le plus petit correspondant aux graines (20 graines).
        /// </summary>
        /// <returns>Le numéro d'un emplacement.</returns>
        public static long CalculerPart1()
        {
            AlmanacP1 almanac = GetAlmanacP1();

            IEnumerable<long> soils = almanac.Seeds.Select(s => Map(s, almanac.SeedToSoil));
            IEnumerable<long> fertilizers = soils.Select(s => Map(s, almanac.SoilToFertilizer));
            IEnumerable<long> waters = fertilizers.Select(s => Map(s, almanac.FertilizerToWater));
            IEnumerable<long> lights = waters.Select(s => Map(s, almanac.WaterToLight));
            IEnumerable<long> temperatures = lights.Select(s => Map(s, almanac.LightToTemperature));
            IEnumerable<long> humidities = temperatures.Select(s => Map(s, almanac.TemperatureToHumidity));
            IEnumerable<long> locations = humidities.Select(s => Map(s, almanac.HumidityToLocation));

            return locations.Min();
        }

        /// <summary>
        /// Pour un Almanach, retourne l'emplacement le plus petit correspondant aux graines (2.5 Milliards de graines).
        /// Version brute-force, prend ~15 minutes à jouer.
        /// </summary>
        /// <returns>Le numéro d'un emplacement.</returns>
        public static long CalculerPart2()
        {
            AlmanacP2 almanac = GetAlmanacP2();

            List<long> seeds = [];

            foreach (var seed in almanac.Seeds)
            {
                for (long i = seed.Start; i < seed.Start + seed.Range; i++)
                {
                    seeds.Add(i);
                }
            }

            IEnumerable<long> soils = seeds.AsParallel().Select(s => Map(s, almanac.SeedToSoil));
            IEnumerable<long> fertilizers = soils.AsParallel().Select(s => Map(s, almanac.SoilToFertilizer));
            IEnumerable<long> waters = fertilizers.AsParallel().Select(s => Map(s, almanac.FertilizerToWater));
            IEnumerable<long> lights = waters.AsParallel().Select(s => Map(s, almanac.WaterToLight));
            IEnumerable<long> temperatures = lights.AsParallel().Select(s => Map(s, almanac.LightToTemperature));
            IEnumerable<long> humidities = temperatures.AsParallel().Select(s => Map(s, almanac.TemperatureToHumidity));
            IEnumerable<long> locations = humidities.AsParallel().Select(s => Map(s, almanac.HumidityToLocation)).ToArray();

            return locations.Min();
        }

        /// <summary>
        /// Pour un Almanach, retourne l'emplacement le plus petit correspondant aux graines (2.5 Milliards de graines).
        /// Version rapide, quelques ms à jouer.
        /// </summary>
        /// <returns>Le numéro d'un emplacement.</returns>
        public static long CalculerPart2Rapide()
        {
            AlmanacP2 almanac = GetAlmanacP2();

            long minLocation;

            foreach (long location in almanac.HumidityToLocation.Select(hl => hl.DestinationRangeStart).OrderBy(dest => dest))
            {
                minLocation = location;

                long reversedHumidity = ReverseMap(minLocation, almanac.HumidityToLocation);
                long reversedTemp = ReverseMap(reversedHumidity, almanac.TemperatureToHumidity);
                long reversedLight = ReverseMap(reversedTemp, almanac.LightToTemperature);
                long reversedWater = ReverseMap(reversedLight, almanac.WaterToLight);
                long reversedFert = ReverseMap(reversedWater, almanac.FertilizerToWater);
                long reversedSoil = ReverseMap(reversedFert, almanac.SoilToFertilizer);
                long reversedSeed = ReverseMap(reversedSoil, almanac.SeedToSoil);

                foreach (var seed in almanac.Seeds)
                {
                    if (reversedSeed >= seed.Start && reversedSeed <= seed.Start + seed.Range)
                    {
                        return minLocation;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Parse l'input et retourne un <see cref="Almanac"/> dont les mappers sont remplis.
        /// </summary>
        /// <returns>Un Almanach.</returns>
        private static Almanac FillMappers()
        {
            Almanac almanac = new();
            List<string> lines = InputHandler.GetInputLines(InputPath);

            const string toSoil = "seed-to-soil";
            const string toFertilizer = "soil-to-fertilizer";
            const string toWater = "fertilizer-to-water";
            const string toLight = "water-to-light";
            const string toTemperature = "light-to-temperature";
            const string toHumidity = "temperature-to-humidity";
            const string toLocation = "humidity-to-location";

            string previousLine = string.Empty;

            foreach (string line in lines)
            {
                previousLine = line.StartsWith(toSoil) ? toSoil : previousLine;
                previousLine = line.StartsWith(toFertilizer) ? toFertilizer : previousLine;
                previousLine = line.StartsWith(toWater) ? toWater : previousLine;
                previousLine = line.StartsWith(toLight) ? toLight : previousLine;
                previousLine = line.StartsWith(toTemperature) ? toTemperature : previousLine;
                previousLine = line.StartsWith(toHumidity) ? toHumidity : previousLine;
                previousLine = line.StartsWith(toLocation) ? toLocation : previousLine;

                switch (previousLine)
                {
                    case toSoil:
                        Mapper? mapS = GetMapper(line);

                        if (mapS != null)
                        {
                            almanac.SeedToSoil.Add(mapS);
                        }
                        break;

                    case toFertilizer:
                        Mapper? mapF = GetMapper(line);

                        if (mapF != null)
                        {
                            almanac.SoilToFertilizer.Add(mapF);
                        }
                        break;

                    case toWater:
                        Mapper? mapW = GetMapper(line);

                        if (mapW != null)
                        {
                            almanac.FertilizerToWater.Add(mapW);
                        }
                        break;

                    case toLight:
                        Mapper? mapL = GetMapper(line);

                        if (mapL != null)
                        {
                            almanac.WaterToLight.Add(mapL);
                        }
                        break;

                    case toTemperature:
                        Mapper? mapT = GetMapper(line);

                        if (mapT != null)
                        {
                            almanac.LightToTemperature.Add(mapT);
                        }
                        break;

                    case toHumidity:
                        Mapper? mapH = GetMapper(line);

                        if (mapH != null)
                        {
                            almanac.TemperatureToHumidity.Add(mapH);
                        }
                        break;

                    case toLocation:
                        Mapper? mapLo = GetMapper(line);

                        if (mapLo != null)
                        {
                            almanac.HumidityToLocation.Add(mapLo);
                        }
                        break;
                }
            }

            return almanac;
        }

        /// <summary>
        /// Parse l'input et retourne un <see cref="AlmanacP1"/>.
        /// </summary>
        /// <returns>Un Almanach.</returns>
        private static AlmanacP1 GetAlmanacP1()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);

            AlmanacP1 almanac = new(FillMappers());

            string line = lines.First(l => l.StartsWith("seeds:"));

            almanac.Seeds = line.Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToList();

            return almanac;
        }

        /// <summary>
        /// Parse l'input et retourne un <see cref="AlmanacP2"/>.
        /// </summary>
        /// <returns>Un Almanach.</returns>
        private static AlmanacP2 GetAlmanacP2()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);

            AlmanacP2 almanac = new(FillMappers());

            string line = lines.First(l => l.StartsWith("seeds:"));

            long[] seedsPairs = line.Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToArray();

            long startingNumber;
            long range;

            for (int i = 0; i < seedsPairs.Length; i += 2)
            {
                startingNumber = seedsPairs[i];
                range = seedsPairs[i + 1];

                almanac.Seeds.Add((startingNumber, range));
            }

            return almanac;
        }

        /// <summary>
        /// Retourne un mapper pour une ligne d'input.
        /// </summary>
        /// <param name="line">Ligne d'entrée.</param>
        /// <returns>Un mapper (null si la ligne d'entrée n'est pas valide).</returns>
        private static Mapper? GetMapper(string line)
        {
            if (!line.Any(char.IsLetter))
            {
                return new Mapper(line.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToArray());
            }

            return null;
        }

        /// <summary>
        /// Retourne un nombre pour un nombre d'entrée et une liste de mappers.
        /// </summary>
        /// <param name="toMap">Input number to map to an output number.</param>
        /// <param name="mappers">Mappers used to map the input number.</param>
        /// <returns>Output number.</returns>
        private static long Map(long toMap, List<Mapper> mappers)
        {
            Mapper? mapper = mappers.Find(m => toMap >= m.SourceRangeStart && toMap <= m.SourceRangeStart + m.RangeLength);

            if (mapper != null)
            {
                return toMap + mapper.GetDifferenceSourceDestination;
            }
            else
            {
                return toMap;
            }
        }

        /// <summary>
        /// Retourne un nombre pour un nombre d'entrée et une liste de mappers.
        /// </summary>
        /// <param name="toMap">Input number to map to an output number.</param>
        /// <param name="mappers">Mappers used to map the input number.</param>
        /// <returns>Output number.</returns>
        private static long ReverseMap(long toReverseMap, List<Mapper> mappers)
        {
            Mapper? mapper = mappers.Find(m => toReverseMap >= m.DestinationRangeStart && toReverseMap <= m.DestinationRangeStart + m.RangeLength);

            if (mapper != null)
            {
                return toReverseMap - mapper.GetDifferenceSourceDestination;
            }
            else
            {
                return toReverseMap;
            }
        }

        /// <summary>
        /// Représente un almanach.
        /// </summary>
        internal class Almanac
        {
            /// <summary>
            /// Liste des mappers fertilisant => eau.
            /// </summary>
            public List<Mapper> FertilizerToWater { get; set; } = [];

            /// <summary>
            /// Liste des mappers humidité => emplacement.
            /// </summary>
            public List<Mapper> HumidityToLocation { get; set; } = [];

            /// <summary>
            /// Liste des mappers lumière => température.
            /// </summary>
            public List<Mapper> LightToTemperature { get; set; } = [];

            /// <summary>
            /// Liste des mappers graine => sol.
            /// </summary>
            public List<Mapper> SeedToSoil { get; set; } = [];

            /// <summary>
            /// Liste des mappers sol => fertilisant.
            /// </summary>
            public List<Mapper> SoilToFertilizer { get; set; } = [];

            /// <summary>
            /// Liste des mappers température => humidité.
            /// </summary>
            public List<Mapper> TemperatureToHumidity { get; set; } = [];

            /// <summary>
            /// Liste des mappers eau => lumière.
            /// </summary>
            public List<Mapper> WaterToLight { get; set; } = [];
        }

        /// <summary>
        /// Représente un almanach de la première partie.
        /// </summary>
        internal class AlmanacP1 : Almanac
        {
            public AlmanacP1(Almanac almanac)
            {
                FertilizerToWater = almanac.FertilizerToWater;
                HumidityToLocation = almanac.HumidityToLocation;
                LightToTemperature = almanac.LightToTemperature;
                SeedToSoil = almanac.SeedToSoil;
                SoilToFertilizer = almanac.SoilToFertilizer;
                TemperatureToHumidity = almanac.TemperatureToHumidity;
                WaterToLight = almanac.WaterToLight;
            }

            /// <summary>
            /// Liste de graines.
            /// </summary>
            public List<long> Seeds { get; set; } = [];
        }

        /// <summary>
        /// Représente un almanach de la seconde partie
        /// </summary>
        internal class AlmanacP2 : Almanac
        {
            public AlmanacP2(Almanac almanac)
            {
                FertilizerToWater = almanac.FertilizerToWater;
                HumidityToLocation = almanac.HumidityToLocation;
                LightToTemperature = almanac.LightToTemperature;
                SeedToSoil = almanac.SeedToSoil;
                SoilToFertilizer = almanac.SoilToFertilizer;
                TemperatureToHumidity = almanac.TemperatureToHumidity;
                WaterToLight = almanac.WaterToLight;
            }

            /// <summary>
            /// Liste de graines, avec leur portée.
            /// </summary>
            public List<(long Start, long Range)> Seeds { get; set; } = [];
        }

        /// <summary>
        /// Représente un mapper source / destination.
        /// </summary>
        /// <param name="map"></param>
        internal class Mapper(long[] map)
        {
            /// <summary>
            /// Début de la plage de destination.
            /// </summary>
            public long DestinationRangeStart => map[0];

            /// <summary>
            /// Retourne la différence entre la source et la destination.
            /// </summary>
            public long GetDifferenceSourceDestination
            { get { return DestinationRangeStart - SourceRangeStart; } }

            /// <summary>
            /// Longueur de la plage.
            /// </summary>
            public long RangeLength => map[2] - 1;

            /// <summary>
            /// Début de la plage de l'origine.
            /// </summary>
            public long SourceRangeStart => map[1];
        }
    }
}