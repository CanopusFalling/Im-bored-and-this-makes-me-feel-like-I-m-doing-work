using System;

using System.Collections.Generic;

namespace Problem_B
{
    class Program
    {
        static void Main(string[] args)
        {
            TradeUnion union = getUnion();
            AddPartners(union);

            Console.WriteLine(union);
        }

        private static TradeUnion getUnion(){
            String unionParameters = Console.ReadLine();

            return new TradeUnion(unionParameters);
        }

        private static void AddPartners(TradeUnion union){
            for (int i = 0; i < union.CountPartnerships(); i++)
            {
                String partnershipInfo = Console.ReadLine();
                String[] split = partnershipInfo.Split(' ');
                union.AddPartners(Convert.ToInt64(split[0]), Convert.ToInt64(split[0]));
            }
        }
    }

    class TradeUnion
    {
        private Country[] members;
        private long partnershipCount;
        private long homeCountry;
        private long leavingCountry;

        public TradeUnion(long c, long p, long x, long l)
        {
            Setup(c, p, x, l);
        }

        public TradeUnion(String unionDefinition){
            String[] parameters = unionDefinition.Split(' ');

            long c = Convert.ToInt64(parameters[0]);
            long p = Convert.ToInt64(parameters[1]);
            long x = Convert.ToInt64(parameters[2]);
            long l = Convert.ToInt64(parameters[3]);

            Setup(c, p, x, l);
        }

        private void Setup(long c, long p, long x, long l)
        {
            members = new Country[c];
            for (long i = 0; i < c; i++)
            {
                members[i] = new Country(i);
                // For the country that has left.
                if(i+1 == l){
                    members[i].ForceLeave();
                }
            }

            this.partnershipCount = p;
            this.homeCountry = x;
            this.leavingCountry = l;
        }

        // ===== Accessors =====
        public override String ToString(){
            String result = "";

            foreach(Country member in members){
                result += member.ToString() + System.Environment.NewLine;
            }

            return result;
        }

        public long CountPartnerships(){
            return this.partnershipCount;
        }

        // ===== Mutators =====
        public void AddPartners(long x, long y){
            Country first = this.members[x];
            Country second = this.members[y];
            first.AddTradeDeal(second);
            second.AddTradeDeal(first);
        }
    }

    class Country
    {
        private long countryID;
        private List<Country> tradeDeals;
        private bool isUnion;

        public Country(long number)
        {
            this.countryID = number;
            this.tradeDeals = new List<Country>();
            this.isUnion = true;
        }

        // ===== Accessors =====
        public override String ToString(){
            return this.GetID() + " " + this.IsUnion();
        }

        public long GetID()
        {
            return this.countryID;
        }

        public bool IsUnion()
        {
            if(isUnion){
            this.updateMembership();
            }
            return this.isUnion;
        }

        // ===== Mutators =====
        public void AddTradeDeal(Country deal)
        {
            this.tradeDeals.Add(deal);
        }

        public void ForceLeave()
        {
            this.isUnion = false;
        }

        // Checks if over half of the trade deals have left yet.
        private void updateMembership()
        {
            float members = this.tradeDeals.Count;
            int leftMembers = 0;

            foreach (Country deal in this.tradeDeals)
            {
                // Add to the count for each member not in the union.
                if (!deal.IsUnion())
                {
                    leftMembers++;
                }
            }

            // Debugging code.
            // Console.WriteLine(leftMembers + " : " + members + " : " + Math.Ceiling(members / 2));
            this.isUnion = !(leftMembers > Math.Ceiling(members / 2));
        }
    }
}
