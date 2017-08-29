﻿using System;

namespace AMC.Domain
{
  public class Product
  {
    public int ID { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public string EditUser { get; set; }
    public DateTime EditDate { get; set; }
  }
}