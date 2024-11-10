﻿namespace Shared.SeedWork
{
	public class MetaData
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public long TotalItems { get; set; }
		public bool HasPrevious { get { return CurrentPage > 1; } }
		public bool HasNext { get { return CurrentPage < TotalPages; } }
		public int FirstRowOnPage { get { return TotalItems > 0 ? (CurrentPage - 1) * PageSize + 1 : 0; } }
		public int LastRowOnPage { get { return (int)Math.Min(CurrentPage * PageSize, TotalItems); } }
	}
}
