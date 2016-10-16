using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    class ModifyCurrency
    {
public static void Insert()
{
    AdventureWorksDataContext context = new AdventureWorksDataContext();

    Currency currency = new Currency();
    currency.CurrencyCode = "PNT";
    currency.Name = "Peanuts";
    currency.ModifiedDate = DateTime.Now;

    context.Currencies.InsertOnSubmit(currency);

    context.SubmitChanges();
}

public static void Update()
{
    AdventureWorksDataContext context = new AdventureWorksDataContext();

    var peanuts = context.Currencies
                    .Where(currency => currency.CurrencyCode == "PNT")
                    .Single();

    peanuts.Name = "Roasted Peanuts";

    context.SubmitChanges();
}

public static void Delete()
{
    AdventureWorksDataContext context = new AdventureWorksDataContext();

    var peanuts = context.Currencies
                    .Where(currency => currency.CurrencyCode == "PNT")
                    .Single();

    context.Currencies.DeleteOnSubmit(peanuts);

    context.SubmitChanges();
}


    }
}
