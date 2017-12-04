using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBK.Web.Api.Models.Equation
{
    public class Customer : BaseResponse
    {
        private string responseCode;
        private string responseDescription;
        private string customerNo;
        private string mnemonic;
        private string location;
        private string firstname;
        private string secondname;
        private string familyname;
        private string fullname;
        private string mobile;
        private string title;
        private string greeting;
        private string addressLine1;
        private string addressLine2;
        private string addressLine3;
        private string addressLine4;
        private string addressLine5;
        private string email;
        private string dob;
        private string homephone;
        private string businessphone;
        private string pager;
        private string industry;
        private string officercode;
        private string officername;
        private string language;
        private string customertype;
        private string branchcode;
        private string branchname;

        [Position(300, 7)]
        public string ResponseCode
        {
            get
            {
                return this.responseCode;
            }
            set
            {
                this.responseCode = value;
            }
        }

        [Position(307, 50)]
        public string ResponseDescription
        {
            get
            {
                return this.responseDescription;
            }
            set
            {
                this.responseDescription = value;
            }
        }

        [Position(357, 6)]
        public string CustomerNo
        {
            get
            {
                return this.customerNo;
            }
            set
            {
                this.customerNo = value;
            }
        }

        [Position(363, 6)]
        public string Mnemonic
        {
            get
            {
                return this.mnemonic;
            }
            set
            {
                this.mnemonic = value;
            }
        }

        [Position(369, 3)]
        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        [Position(372, 15)]
        public string FirstName
        {
            get
            {
                return this.firstname;
            }
            set
            {
                this.firstname = value;
            }
        }

        [Position(387, 15)]
        public string SecondName
        {
            get
            {
                return this.secondname;
            }
            set
            {
                this.secondname = value;
            }
        }

        [Position(402, 15)]
        public string FamilyName
        {
            get
            {
                return this.familyname;
            }
            set
            {
                this.familyname = value;
            }
        }

        [Position(417, 35)]
        public string FullName
        {
            get
            {
                return this.fullname;
            }
            set
            {
                this.fullname = value;
            }
        }

        [Position(452, 35)]
        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        [Position(487, 3)]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        [Position(490, 35)]
        public string Greeting
        {
            get
            {
                return this.greeting;
            }
            set
            {
                this.greeting = value;
            }
        }

        [Position(525, 35)]
        public string AddressLine1
        {
            get
            {
                return this.addressLine1;
            }
            set
            {
                this.addressLine1 = value;
            }
        }

        [Position(560, 35)]
        public string AddressLine2
        {
            get
            {
                return this.addressLine2;
            }
            set
            {
                this.addressLine2 = value;
            }
        }

        [Position(595, 35)]
        public string AddressLine3
        {
            get
            {
                return this.addressLine3;
            }
            set
            {
                this.addressLine3 = value;
            }
        }

        [Position(630, 35)]
        public string AddressLine4
        {
            get
            {
                return this.addressLine4;
            }
            set
            {
                this.addressLine4 = value;
            }
        }

        [Position(665, 35)]
        public string AddressLine5
        {
            get
            {
                return this.addressLine5;
            }
            set
            {
                this.addressLine5 = value;
            }
        }

        [Position(700, 60)]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        [Position(760, 6)]
        public string DateOfBirth
        {
            get
            {
                return this.dob;
            }
            set
            {
                this.dob = value;
            }
        }

        [Position(766, 35)]
        public string HomePhone
        {
            get
            {
                return this.homephone;
            }
            set
            {
                this.homephone = value;
            }
        }

        [Position(801, 35)]
        public string BusinessPhone
        {
            get
            {
                return this.businessphone;
            }
            set
            {
                this.businessphone = value;
            }
        }

        [Position(836, 35)]
        public string Pager
        {
            get
            {
                return this.pager;
            }
            set
            {
                this.pager = value;
            }
        }

        [Position(871, 2)]
        public string Industry
        {
            get
            {
                return this.industry;
            }
            set
            {
                this.industry = value;
            }
        }

        [Position(908, 3)]
        public string OfficerCode
        {
            get
            {
                return this.officercode;
            }
            set
            {
                this.officercode = value;
            }
        }

        [Position(911, 35)]
        public string OfficerName
        {
            get
            {
                return this.officername;
            }
            set
            {
                this.officername = value;
            }
        }

        [Position(946, 2)]
        public string PreferredLanguage
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
            }
        }


        [Position(948, 2)]
        public string CustomerType
        {
            get
            {
                return this.customertype;
            }
            set
            {
                this.customertype = value;
            }
        }

        [Position(950, 4)]
        public string BranchCode
        {
            get
            {
                return this.branchcode;
            }
            set
            {
                this.branchcode = value;
            }
        }

        [Position(954, 35)]
        public string BranchName
        {
            get
            {
                return this.branchname;
            }
            set
            {
                this.branchname = value;
            }
        }

        public double ResponseTime { get; set; }
    }
}
