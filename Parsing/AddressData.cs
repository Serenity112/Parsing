using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;


namespace Parser
{
    class AddressData
    {
        public string city;
        public string street;
        public int house;
        public int floor;

        public AddressData(string city, string street, int house, int floor)
        {
            this.city = city;
            this.street = street;
            this.house = house;
            this.floor = floor;
        }

        public AddressData()
        {
            this.city = null;
            this.street = null;
            this.house = 0;
            this.floor = 0;
        }

        public override bool Equals(object obj) => this.Equals(obj as AddressData);

        public bool Equals(AddressData data)
        {
            if (data is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, data))
            {
                return true;
            }

            if (this.GetType() != data.GetType())
            {
                return false;
            }

            return (city == data.city && street == data.street && house == data.house && floor == data.floor);
        }

        public override int GetHashCode() => (city, street, house, floor).GetHashCode();

        public static bool operator ==(AddressData data1, AddressData data2)
        {
            if (data1 is null)
            {
                if (data2 is null)
                {
                    return true;
                }

                return false;
            }

            return data1.Equals(data2);
        }

        public static bool operator !=(AddressData data1, AddressData data2) => !(data1 == data2);

        public override string ToString()
        {
            return city + " " + street + " " + house + " " + floor;
        }
    }
}
