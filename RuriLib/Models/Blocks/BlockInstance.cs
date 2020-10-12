﻿using RuriLib.Helpers.LoliCode;
using RuriLib.Models.Blocks.Settings;
using RuriLib.Models.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RuriLib.Models.Blocks
{
    public abstract class BlockInstance
    {
        public string Id { get; set; }
        public bool Disabled { get; set; } = false;
        public string Label { get; set; }
        public string ReadableName { get; set; }
        public List<BlockSetting> Settings { get; set; } = new List<BlockSetting>();
        public BlockDescriptor Descriptor { get; set; }

        public virtual string ToLC()
        {
            /*
             * BLOCK:BlockId
             * DISABLED
             * LABEL:My Label
             */

            using var writer = new LoliCodeWriter();

            if (Disabled)
                writer.WriteLine("DISABLED");

            if (Label != ReadableName)
                writer.WriteLine($"LABEL:{Label}");

            // Write all the settings
            foreach (var setting in Settings)
            {
                var param = Descriptor.Parameters.FirstOrDefault(p => p.Name == setting.Name);

                if (param == null)
                    throw new Exception($"This setting is not a valid input parameter: {setting.Name}");

                writer.AppendSetting(setting, param);
            }

            return writer.ToString();
        }

        public virtual void FromLC(ref string script)
        {
            /*
             * DISABLED
             * LABEL:My Label
             * ...
             */

            using var reader = new StringReader(script);
            using var writer = new StringWriter();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("DISABLED"))
                    Disabled = true;

                else if (line.StartsWith("LABEL:"))
                {
                    var match = Regex.Match(line, $"^LABEL:(.*)$");
                    Label = match.Groups[1].Value;
                }

                else
                {
                    writer.WriteLine(line);
                }
            }

            // Edit the original script that is passed down the pipeline
            script = writer.ToString();
        }

        public virtual string ToCSharp(List<string> definedVariables, ConfigSettings settings) => throw new NotImplementedException();
    }
}
