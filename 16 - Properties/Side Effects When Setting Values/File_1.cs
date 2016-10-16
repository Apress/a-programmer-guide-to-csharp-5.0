// 18 - Properties\Side Effects When Setting Values
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
class Basket
{
    internal void UpdateTotal()
    {
        total = 0;
        foreach (BasketItem item in items)
        {
            total += item.Total;
        }
    }
    
    ArrayList    items = new ArrayList();
    Decimal    total;
}
class BasketItem
{
    BasketItem(Basket basket)
    {
        this.basket = basket;
    }
    public int Quantity
    {
        get
        {
            return(quantity);
        }
        set
        {
            quantity = value;
            basket.UpdateTotal();
        }
    }
    public Decimal Price
    {
        get
        {
            return(price);
        }
        set
        {
            price = value;
            basket.UpdateTotal();
        }
    }
    public Decimal Total
    {
        get
        {
            // volume discount; 10% if 10 or more are purchased
            if (quantity >= 10)
            return(quantity * price * 0.90m);
            else
            return(quantity * price); 
        }
    }
    
    int        quantity;     // count of the item
    Decimal    price;        // price of the item
    Basket     basket;       // reference back to the basket
}