using System;
using System.Collections.Generic;

namespace AMC.Domain
{
  public class Supplier
  {
    public int ID { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string ContactNo { get; set; }
    public string CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public string EditUser { get; set; }
    public DateTime EditDate { get; set; }
  }
}
