﻿using System;
using System.Collections.Generic;
using System.Linq;
using scrypt.CommandLine;
using scrypt.Utils;

namespace scrypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try { Process(args); }
            catch (Exception e)
            {
                Terminal.Out(Terminal.Theme.Error, e.Message);
#if DEBUG
                Terminal.Out(Terminal.Theme.Input, e.StackTrace);
#endif
            }
        }

        private static void Output(IEnumerable<string> values, bool verbose)
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrEmpty(value))
                    Terminal.Out(Terminal.Theme.Output, verbose ? "{0} : {1}" : "{0}", value, value.Length);
            }
        }

        private static void Process(string[] args)
        {
            var verbose = args.Exists(Const.CommandPrefix, "verbose", "v");
            var options = Options.GetAll(args);
            var junk = Combine(args);
            ExecuteAll(options, junk, verbose);
            Output(junk, verbose);
        }

        private static IList<string> Combine(params string[] values)
        {
            return values.String()
                .Split(values.Parse(Const.CommandPrefix + @"\S+"), StringSplitOptions.RemoveEmptyEntries)
                .ReplaceAll(Const.AliasPrefix, Const.GetValue)
                .ToList()
                .Append(Console.IsInputRedirected ? Console.In.ReadToEnd().Cleanse() : null);
        }

        private static void ExecuteAll(IEnumerable<Option> options, IList<string> values, bool verbose)
        {
            for (int i = 0; i < values.Count; i++)
            {
                foreach (var option in options)
                {
                    if (option is Param)
                        values[i] = Execute(option as Param, values[i], verbose);
                }
            }
        }

        private static string Execute(Param option, string value, bool verbose)
        {
            switch (option.Type)
            {
                case Enums.ParamType.Command:
                    return verbose
                        ? option.Verbose(value, null)
                        : option.Method(value, null);

                case Enums.ParamType.Crypto:
                    var key = Terminal.In("'{0}' {1} key ", value.Limit(), option.Cmds.Last());
                    return verbose
                        ? option.Verbose(value, key)
                        : option.Method(value, key);
            }

            return value;
        }
    }
}