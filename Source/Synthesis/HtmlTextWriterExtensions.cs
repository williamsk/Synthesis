﻿namespace Synthesis
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web.UI;
	using Synthesis.FieldTypes;
using Sitecore.Web.UI.WebControls;

	public static class HtmlTextWriterExtensions
	{
		/// <summary>
		/// Renders a link to the file field as a hyperlink tag. Rendering directives in the Action parameter will construct the body of the link. You may add attributes to the link by adding them to the writer before invoking this method.
		/// </summary>
		public static void RenderFileFieldLink(this HtmlTextWriter writer, FileField fileField, Action linkBody)
		{
			if (!fileField.HasValue) return;

			writer.AddAttribute("href", fileField.Url);
			writer.RenderBeginTag("a");
			linkBody();
			writer.RenderEndTag();
		}

		/// <summary>
		/// Renders a Synthesis Hyperlink Field to a HtmlTextWriter.
		/// </summary>
		public static void RenderLinkField(this HtmlTextWriter writer, HyperlinkField linkField)
		{
			RenderLinkField(writer, linkField, x => { });
		}

		/// <summary>
		/// Renders a Synthesis Hyperlink Field to a HtmlTextWriter and configures parameters of the field renderer
		/// </summary>
		public static void RenderLinkField(this HtmlTextWriter writer, HyperlinkField linkField, Action<Link> parameters)
		{
			if (linkField.HasValue || Sitecore.Context.PageMode.IsPageEditor)
			{
				Link link = new Link();
				link.AttachToLinkField(linkField);
				parameters(link);

				link.RenderControl(writer);
			}
		}

		/// <summary>
		/// Renders the image using field renderer. Nothing is rendered if the image has no value and we aren't page editing.
		/// </summary>
		public static void RenderImageField(this HtmlTextWriter writer, ImageField imageField)
		{
			RenderImageField(writer, imageField, x => { });
		}

		/// <summary>
		/// Renders the image to a given HtmlTextWriter using field renderer. Uses Sitecore image scaling if max width or height is passed (pass null for only one dimension). Nothing is rendered if the image has no value and we aren't page editing. 
		/// </summary>
		/// <param name="maxWidth">Maximum width for the image. May be less if it has a portrait aspect ratio. Pass null to scale by height only.</param>
		/// <param name="maxHeight">Maximum height for the image. May be less if it has a landscape aspect ratio. Pass null to scale by width only.</param>
		public static void RenderImageField(this HtmlTextWriter writer, ImageField imageField, int? maxWidth, int? maxHeight)
		{
			RenderImageField(writer, imageField, image =>
			{
				if (maxWidth.HasValue)
					image.MaxWidth = maxWidth.Value;

				if (maxHeight.HasValue)
					image.MaxHeight = maxHeight.Value;
			});
		}

		/// <summary>
		/// Renders the image to a given HtmlTextWriter using field renderer. Nothing is rendered if the image has no value and we aren't page editing. 
		/// </summary>
		/// <param name="parameters">Action to execute to configure parameters on the output control</param>
		public static void RenderImageField(this HtmlTextWriter writer, ImageField imageField, Action<Image> parameters)
		{
			var image = new Sitecore.Web.UI.WebControls.Image();

			WebControlExtensions.AttachToImageField(image, imageField);

			parameters(image);

			image.RenderControl(writer);
		}
	}
}
