using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities.Customer
{
    public class CustomerEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;

        private CustomerEntity() { }

        public CustomerEntity(string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentNullException(nameof(phone));

            this.Name = name;
            this.Email = email;
            this.Phone = phone;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }
        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            this.Email = email;
        }

        public void UpdatePhone(string phone)
        {
            // taken from the internet
            var validatePhoneNumberExp = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");

            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentNullException(nameof(phone));

            if (!validatePhoneNumberExp.IsMatch(phone)) throw new FormatException($"nameof(phone) parameter not a valid phone number");

            this.Phone = phone;
        }
    }
}