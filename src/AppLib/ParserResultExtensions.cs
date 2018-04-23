using System.Linq;
using CommandLine;

namespace AppLib
{
    public static class ParserResultExtensions
    {
        public static bool IsRequestToShowHelp<T>(this ParserResult<T> parser)
        {
            return SingleErrorWithTag(parser, ErrorType.HelpRequestedError);
        }

        public static bool IsRequestToShowVerbHelp<T>(this ParserResult<T> parser)
        {
            return SingleErrorWithTag(parser, ErrorType.HelpVerbRequestedError);
        }

        public static bool IsRequestToShowVersion<T>(this ParserResult<T> parser)
        {
            return SingleErrorWithTag(parser, ErrorType.VersionRequestedError);
        }

        private static bool SingleErrorWithTag<T>(ParserResult<T> parser, ErrorType tag)
        {
            if (parser == null || parser.Tag != ParserResultType.NotParsed)
            {
                return false;
            }

            var requestToShowHelp = false;
            parser.WithNotParsed(errors =>
            {
                var errorList = errors.ToList();
                requestToShowHelp = errorList.Count == 1 && errorList[0].Tag == tag;
            });
            return requestToShowHelp;
        }
    }
}