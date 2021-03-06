﻿using System;
using System.Linq;
using System.Text;
using Synthesis.FieldTypes;
using System.Web;
using Sitecore.Web.UI.WebControls;
using RazorEngine.Text;
using Synthesis;

namespace Blade.Razor.Helpers
{
	public static class SynthesisHtmlHelper
	{
		public static IEncodedString DateTimeFor<T>(this BladeHtmlHelper<T> helper, Func<T, DateTimeField> selector)
		{
			return DateTimeFor(helper, selector, "g");
		}

		public static IEncodedString DateTimeFor<T>(this BladeHtmlHelper<T> helper, Func<T, DateTimeField> selector, string format)
		{
			return DateTimeFor(helper, selector, x => { x.Format = format; });
		}

		public static IEncodedString DateTimeFor<T>(this BladeHtmlHelper<T> helper, Func<T, DateTimeField> selector, Action<Date> parameters)
		{
			var field = selector(helper.View.Model);

			if (field.HasValue || helper.View.IsEditing)
			{
				Date date = new Date();
				date.AttachToDateTimeField(field);
				parameters(date);

				return new RawString(date.RenderAsText());
			}

			return new RawString(string.Empty);
		}

		public static IEncodedString FileLinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, FileField> selector)
		{
			return FileLinkFor(helper, selector, null);
		}

		public static IEncodedString FileLinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, FileField> selector, string linkText)
		{
			return FileLinkFor(helper, selector, linkText, new { });
		}

		public static IEncodedString FileLinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, FileField> selector, string linkText, object attributes)
		{
			var field = selector(helper.View.Model);

			if (field.HasValue)
			{
				var sb = new StringBuilder();
				sb.Append("<a href=\"");
				sb.Append(HttpUtility.HtmlEncode(field.Url));
				sb.Append("\"");

				foreach (var attribute in attributes.GetType().GetProperties().Where(x => x.CanRead))
				{
					sb.AppendFormat(" {0}=\"{1}\"", attribute.Name.Replace('_', '-'), HttpUtility.HtmlEncode(attribute.GetValue(attributes, null)));
				}

				sb.Append(">");
				sb.Append(linkText ?? field.Url);
				sb.Append("</a>");

				return new RawString(sb.ToString());
			}

			return new RawString(string.Empty);
		}

		public static IEncodedString ImageFor<T>(this BladeHtmlHelper<T> helper, Func<T, ImageField> selector)
		{
			return ImageFor(helper, selector, x => { });
		}

		public static IEncodedString ImageFor<T>(this BladeHtmlHelper<T> helper, Func<T, ImageField> selector, string cssClass)
		{
			return ImageFor(helper, selector, x => { x.CssClass = cssClass; });
		}

		public static IEncodedString ImageFor<T>(this BladeHtmlHelper<T> helper, Func<T, ImageField> selector, int maxWidth, int maxHeight)
		{
			return ImageFor(helper, selector, x =>
			{
				x.MaxWidth = maxWidth;
				x.MaxHeight = maxHeight;
			});
		}

		public static IEncodedString ImageFor<T>(this BladeHtmlHelper<T> helper, Func<T, ImageField> selector, int maxWidth, int maxHeight, string cssClass)
		{
			return ImageFor(helper, selector, x =>
			{
				x.MaxWidth = maxWidth;
				x.MaxHeight = maxHeight;
				x.CssClass = cssClass;
			});
		}

		public static IEncodedString ImageFor<T>(this BladeHtmlHelper<T> helper, Func<T, ImageField> selector, Action<Image> parameters)
		{
			var field = selector(helper.View.Model);

			if (field.HasValue || helper.View.IsEditing)
			{
				Image imageRenderer = new Image();
				imageRenderer.AttachToImageField(field);
				parameters(imageRenderer);

				return new RawString(imageRenderer.RenderAsText());
			}

			return new RawString(string.Empty);
		}

		public static IEncodedString TextFor<T>(this BladeHtmlHelper<T> helper, Func<T, TextField> selector)
		{
			return TextFor(helper, selector, true);
		}

		public static IEncodedString TextFor<T>(this BladeHtmlHelper<T> helper, Func<T, TextField> selector, bool editable)
		{
			var field = selector(helper.View.Model);

			if (field.HasTextValue || helper.View.IsEditing)
			{
				if (editable)
					return new RawString(field.RenderedValue);

				var richText = field as RichTextField;
				
				if (richText != null) return new RawString(richText.ExpandedLinksValue);
				
				return new RawString(field.RawValue);
			}

			return new RawString(string.Empty);
		}

		public static IEncodedString HyperlinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, HyperlinkField> selector)
		{
			return HyperlinkFor(helper, selector, x => { });
		}

		public static IEncodedString HyperlinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, HyperlinkField> selector, string linkText)
		{
			return HyperlinkFor(helper, selector, x =>
			{
				x.Text = linkText;
			});
		}

		public static IEncodedString HyperlinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, HyperlinkField> selector, string linkText, string cssClass)
		{
			return HyperlinkFor(helper, selector, x =>
			{
				x.Text = linkText;
				x.CssClass = cssClass;
			});
		}

		public static IEncodedString HyperlinkFor<T>(this BladeHtmlHelper<T> helper, Func<T, HyperlinkField> selector, Action<Link> parameters)
		{
			var field = selector(helper.View.Model);

			if (field.HasValue || helper.View.IsEditing)
			{
				Link link = new Link();
				link.AttachToLinkField(field);
				parameters(link);

				return new RawString(link.RenderAsText());
			}

			return new RawString(string.Empty);
		}
	}
}