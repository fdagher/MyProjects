using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Position : System.Attribute
    {
        private int startpos;
        private int length;

        public Position(int startpos, int length)
        {
            this.startpos = startpos;
            this.length = length;
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public int StartPos
        {
            get
            {
                return this.startpos;
            }
        }
    }


    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)]
    public class MessageDefinition : System.Attribute
    {
        private int length;

        public MessageDefinition(int length)
        {
           this.length = length;
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }
    }


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class RepeatData : System.Attribute
    {
        private int startpos;
        private int length;

        public RepeatData(int startpos, int length)
        {
            this.startpos = startpos;
            this.length = length;
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public int StartPos
        {
            get
            {
                return this.startpos;
            }
        }
    }


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class RepeatCount : System.Attribute
    {
        private int startpos;
        private int length;

        public RepeatCount(int startpos, int length)
        {
            this.startpos = startpos;
            this.length = length;
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public int StartPos
        {
            get
            {
                return this.startpos;
            }
        }
    }

}
