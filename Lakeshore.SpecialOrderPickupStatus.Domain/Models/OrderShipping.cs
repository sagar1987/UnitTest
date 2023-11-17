using Lakeshore.SpecialOrderPickupStatus.Domain;
using Lakeshore.SpecialOrderPickupStatus.Domain.Har.Events;
using Lakeshore.SpecialOrderPickupStatus.Dto.SpecialOrderPickupStatus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lakeshore.SpecialOrderPickupStatus.Domain.Models;

public class OrderShipping : Entity
{
    public OrderShipping()
    {
        //for EF
    }
    [Key]
    public decimal StoreTransactionNumber { get; private set; } // need
    [Key]
    public int StoreNumber { get; private set; } // need
    [Key]
    public DateTime EntryDateTime { get; private set; }
    [Key]
    public decimal SequenceNumber { get; private set; }

    public string? OrderType { get; private set; } // need

 
    public decimal? Es_Order_Id { get; private set; } // need

 
    public string? Bart_Status { get;  set; } // need

    public DateTime? BartProcessedDatetime { get;  set; } // need

    public void StatusUpdate(SpecialOrderDto? specialOrderDto)
    {
        if (specialOrderDto != null)
            this.AddDomainEvent(new SpecialOrderUpdatedDomainEvent(specialOrderDto));
    }

    public OrderShipping(decimal storeTransactionNumber, int storeNumber, DateTime entryDateTime, decimal sequenceNumber, string? orderType,
        decimal? es_Order_Id,string? bart_Status,
        DateTime? bartProcessedDatetime)
    {
        StoreTransactionNumber = storeTransactionNumber;
        StoreNumber = storeNumber;
        EntryDateTime = entryDateTime;
        SequenceNumber = sequenceNumber;
        OrderType = orderType;
        Es_Order_Id = es_Order_Id;
        Bart_Status = bart_Status;
        BartProcessedDatetime = bartProcessedDatetime;
    }

}
