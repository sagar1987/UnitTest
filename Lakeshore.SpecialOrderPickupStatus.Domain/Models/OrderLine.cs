using System;
using System.Collections.Generic;
using Lakeshore.SpecialOrderPickupStatus.Domain;
using System.ComponentModel.DataAnnotations;

namespace Lakeshore.SpecialOrderPickupStatus.Domain.Models;

public class OrderLine : Entity
{
    public OrderLine()
    {
        //for EF
    }
    [Key]
    public decimal StoreTransactionNumber { get; private set; } // need
    [Key]
    public int StoreNumber { get; private set; } // need
    [Key]
    public DateTime EntryDateTime { get; private set; }

    public decimal StoreLineId { get; private set; } // need

    public string? ItemNumber { get; private set; } // need

    public int Quantity { get; private set; } // need

   
    public OrderLine(decimal storeTransactionNumber, int storeNumber, DateTime entryDateTime, decimal storeLineId, string? itemNumber, int quantity)
    {
        StoreTransactionNumber = storeTransactionNumber;
        StoreNumber = storeNumber;
        EntryDateTime = entryDateTime;
        StoreLineId = storeLineId;
        ItemNumber = itemNumber;
        Quantity = quantity;
    }

}
