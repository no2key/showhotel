//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PettiInn.DAL.Entities.EF5
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomBookingCancel : EntityBase
    {
        public string Comment { get; set; }
        public System.DateTime CancelDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        public virtual RoomBooking RoomBooking { get; set; }
    }
}