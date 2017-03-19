using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace uniform_interface.Formatters
{
    public class CsvOutputFormatter : OutputFormatter
    {
        private readonly CsvFormatterOptions _options;


        public string ContentType { get; private set; }

        public CsvOutputFormatter(CsvFormatterOptions csvFormatterOptions = null)
        {
            ContentType = "text/csv";
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/csv"));

            if (csvFormatterOptions == null)
            {
                csvFormatterOptions = new CsvFormatterOptions();
            }

            _options = csvFormatterOptions;

            //SupportedEncodings.Add(Encoding.GetEncoding("utf-8"));
        }

        protected override bool CanWriteType(Type type)
        {

            if (type == null)
                throw new ArgumentNullException("type");

            return isTypeOfIEnumerable(type);
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return (context.HttpContext.Request.ContentType == ContentType);
        }

        private bool isTypeOfIEnumerable(Type type)
        {

            foreach (Type interfaceType in type.GetInterfaces())
            {

                if (interfaceType == typeof(IList))
                    return true;
            }

            return false;
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {

            var response = context.HttpContext.Response;

            Type type = context.Object.GetType();
            Type itemType;

            if (type.GetGenericArguments().Length > 0)
            {
                itemType = type.GetGenericArguments()[0];
            }
            else
            {
                itemType = type.GetElementType();
            }

            StringWriter _stringWriter = new StringWriter();

            if (_options.UseSingleLineHeaderInCsv)
            {
                _stringWriter.WriteLine(
                    string.Join<string>(
                        _options.CsvDelimiter, itemType.GetProperties().Select(x => x.Name)
                    )
                );
            }

            foreach (var obj in (IEnumerable<object>)context.Object)
            {

                var vals = obj.GetType().GetProperties().Select(
                    pi => new
                    {
                        Value = pi.GetValue(obj)
                    }
                );

                string _valueLine = string.Empty;

                foreach (var val in vals)
                {

                    if (val.Value != null)
                    {

                        var _val = val.Value.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (_val.Contains(","))
                            _val = string.Concat("\"", _val, "\"");

                        //Replace any \r or \n special characters from a new line with a space
                        if (_val.Contains("\r"))
                            _val = _val.Replace("\r", " ");
                        if (_val.Contains("\n"))
                            _val = _val.Replace("\n", " ");

                        _valueLine = string.Concat(_valueLine, _val, _options.CsvDelimiter);

                    }
                    else
                    {
                        _valueLine = string.Concat(_valueLine, string.Empty, _options.CsvDelimiter);
                    }
                }

                _stringWriter.WriteLine(_valueLine.TrimEnd(_options.CsvDelimiter.ToCharArray()));
            }

            var streamWriter = new StreamWriter(response.Body);
            await streamWriter.WriteAsync(_stringWriter.ToString());
            await streamWriter.FlushAsync();

        }
    }

    public class CsvFormatterOptions
    {
        public bool UseSingleLineHeaderInCsv { get; set; } = true;

        public string CsvDelimiter { get; set; } = ";";
    }
}