using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	//public class ModelWithChildren
	//{
	//	public int ChildCount { get; set; }
	//	public List<ModelWithChildren> Children { get; set; }
	//	public string Label { get; set; }
	//	public ModelWithChildren Parent { get; set; }
	//	public string ParentLabel => Parent == null ? "none" : Parent.Label;

	//	public static List<ModelWithChildren> GetModel()
	//	{
	//		var childModel = new List<ModelWithChildren>();
	//		childModel.Add(new ModelWithChildren { ChildCount = 0, Label = "B", Parent = null });

	//		var model = new List<ModelWithChildren>();
	//		model.Add(new ModelWithChildren { ChildCount = 1, Label = "A", Parent = null, Children = childModel });
	//		return model;
	//	}
	//}


	class TransactionView
	{
		public string Hierarchy;        // Date, Counterparty, InvoiceLine
		public List<TransactionView> Children;
		public string Amount;
		public string Comment;
		public string Acount;
		public string Balance;
		public string Currency;

		public bool HasChildren => Children != null && Children.Count != 0;
	}
}
