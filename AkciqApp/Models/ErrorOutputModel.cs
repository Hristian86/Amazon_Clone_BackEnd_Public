namespace AkciqApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ErrorOutputModel
    {

        public ErrorOutputModel(string error)
        {
            this.Error = error;
        }

        public ErrorOutputModel(IList<string> errors)
        {
            this.Errors = errors;
        }

        public IEnumerable<string> Errors { get; }

        public string Error { get; }
    }
}
