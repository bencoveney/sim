using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using OpenTK.Input;
using Sim.Utils;

namespace Sim.Input
{
    public class KeyMappings
    {
        private readonly Dictionary<string, KeyMapping> mappingsByTrigger = new();
        private readonly Dictionary<Key, List<KeyMapping>> mappingsByKey = new();
        private const string KEY_MAPPINGS_RESOURCES = "keyMappings.json";

        public KeyMappings()
        {
            try
            {
                var mappingsFile = Resource.ReadString(KEY_MAPPINGS_RESOURCES, Resource.Kind.Data);
                using var document = JsonDocument.Parse(mappingsFile);
                var mappings = document.RootElement.GetProperty("mappings").EnumerateArray().Select((mapping, index) =>
                {
                    var name = mapping.GetProperty("name").GetString();
                    var trigger = mapping.GetProperty("trigger").GetString();
                    try
                    {
                        var defaultKey = Enum.Parse<Key>(mapping.GetProperty("defaultKey").GetString());
                        return new KeyMapping { DisplayName = name, DisplayOrder = index, Trigger = trigger, DefaultKey = defaultKey, AssignedKey = defaultKey };
                    }
                    catch (ArgumentException e)
                    {
                        throw new Exception($"Unable to parse defaultKey for {name}", e);
                    }
                });

                foreach (var mapping in mappings)
                {
                    mappingsByTrigger.Add(mapping.Trigger, mapping);
                    AddMappingByKey(mapping);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to parse {KEY_MAPPINGS_RESOURCES}", e);
            }
        }

        public KeyMapping GetMappingByTrigger(string trigger)
        {
            return mappingsByTrigger[trigger];
        }

        public IEnumerable<KeyMapping> GetMappingsByKey(Key key)
        {
            return mappingsByKey[key];
        }

        public IEnumerable<KeyMapping> GetMappings()
        {
            return mappingsByTrigger.Values;
        }

        public void ReassignKey(string trigger, Key key)
        {
            var mapping = mappingsByTrigger[trigger];

            mappingsByKey[mapping.AssignedKey].Remove(mapping);

            mapping.AssignedKey = key;
            AddMappingByKey(mapping);
        }

        private void AddMappingByKey(KeyMapping mapping)
        {
            if (!mappingsByKey.ContainsKey(mapping.AssignedKey))
            {
                mappingsByKey[mapping.AssignedKey] = new();
            }
            mappingsByKey[mapping.AssignedKey].Add(mapping);
        }
    }

    public class KeyMapping
    {
        public string DisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public string Trigger { get; set; }
        public Key DefaultKey { get; set; }
        public Key AssignedKey { get; set; }
    }
}