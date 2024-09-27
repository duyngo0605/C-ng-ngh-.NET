using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;
    [Required]
    public int? CategoryId { get; set; }
    [Required]
    public short? UnitsInStock { get; set; }
    [Required]
    public decimal? UnitPrice { get; set; }

    public virtual Category? Category { get; set; }
}
