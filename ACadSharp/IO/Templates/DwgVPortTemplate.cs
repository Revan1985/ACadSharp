﻿using ACadSharp.IO.DWG;
using ACadSharp.Tables;

namespace ACadSharp.IO.Templates
{
	internal class DwgVPortTemplate : CadTemplate<VPort>
	{
		public ulong VportControlHandle { get; set; }
		
		public ulong? BackgroundHandle { get; set; }
		
		public ulong? StyleHandle { get; set; }
		
		public ulong? SunHandle { get; set; }
		
		public ulong? NamedUcsHandle { get; set; }

		public ulong? BaseUcsHandle { get; set; }

		public DwgVPortTemplate(VPort cadObject) : base(cadObject) { }

		public override bool AddHandle(int dxfcode, ulong handle)
		{
			bool value = base.AddHandle(dxfcode, handle);
			if (value)
				return value;

			switch (dxfcode)
			{
				case 348:
					this.StyleHandle = handle;
					value = true;
					break;
				default:
					break;
			}

			return value;
		}

		public override void Build(CadDocumentBuilder builder)
		{
			//TODO: implement DwgVPortTemplate

			base.Build(builder);

			if (this.BaseUcsHandle.HasValue)
			{
				this.CadObject.BaseUcs = builder.GetCadObject<UCS>(this.BaseUcsHandle.Value);
			}

			if (this.NamedUcsHandle.HasValue)
			{
				this.CadObject.NamedUcs = builder.GetCadObject<UCS>(this.NamedUcsHandle.Value);
			}

			if (builder.TryGetCadObject(StyleHandle, out CadObject style))
			{

			}
		}
	}
}
