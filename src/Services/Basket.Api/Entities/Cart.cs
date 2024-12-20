﻿namespace Basket.Api.Entities
{
	public class Cart
	{
		public string Username { get; set; }
		public List<CartItem> Items { get; set; } = new();

		public Cart() { }

		public Cart(string username)
		{
			Username = username;
		}

		public decimal TotalPrice
		{
			get { return Items.Sum(item => item.Quantity * item.ItemPrice); }
		}
	}
}
