﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CH.Alika.Json.Server.Model
{
    class RecordOptions : IRecord, IOptions
    {
        private readonly IFieldNameTranslator _fieldNameXlator = new DefaultFieldNameTranslator();
        private readonly Dictionary<string, string> _options = new Dictionary<string, string>();

        public RecordOptions(IDataRecord record)
        {
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (_fieldNameXlator.IsIgnoredFieldForJson(record, i))
                    continue;

                _options[record.GetName(i).ToLower()] = record.GetValue(i).ToString().ToLower();
            }
        }

        public void ApplyTo(IDataContainer parent)
        {
            throw new NotImplementedException();
        }

        public bool IsNotPartOfCollection()
        {
            throw new NotImplementedException();
        }

        public bool IsArray { get { return !_options.ContainsKey("isarray") || "1".Equals(_options["isarray"]); }}
        public bool IsFormatted { get { return !_options.ContainsKey("isformatted") || "1".Equals(_options["isformatted"]); } }
        public bool IsArrayOfArray { get { return _options.ContainsKey("isarrayofarray") && "1".Equals(_options["isarrayofarray"]); } }
    }
}
