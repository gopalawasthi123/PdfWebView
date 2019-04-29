using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace samplewebviewpdf
{
    [Activity(Label = "WebView")]
    public class WebViewPdf : Activity
    {
        WebView webView;
        LinearLayout linearLayout;
       public ImageView imgViewBack, imgViewForward, imgRefresh;
        TextView txtGlobeIcon;
        string externalurl = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.webviewlayout);

            // Create your application here
            linearLayout = FindViewById<LinearLayout>(Resource.Id.LytForExternalUrl);
            webView = FindViewById<WebView>(Resource.Id.webView1);
            imgViewBack = FindViewById<ImageView>(Resource.Id.imgViewBack);
            imgViewForward = FindViewById<ImageView>(Resource.Id.imgViewForward);
            imgRefresh = FindViewById<ImageView>(Resource.Id.imgRefresh);
            txtGlobeIcon = FindViewById<TextView>(Resource.Id.txtGlobeIcon);
            // externalurl = "https://www.adobe.com/content/dam/acom/en/devnet/acrobat/pdfs/pdf_open_parameters.pdf";

            //SetWebView(webView, externalurl);

            imgViewBack.Click += (sender, e) => {
                if (webView.CanGoBack())
                {
                    webView.GoBack();
                }
            };
            imgViewForward.Click += (sender, e) => {
                if (webView.CanGoForward())
                {
                    webView.GoForward();
                }
            };

            txtGlobeIcon.Click += (sender, e) => {
                webView.Reload();
                var browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(externalurl));
                StartActivity(browserIntent);
            };

            imgRefresh.Click += (sender, e) => webView.Reload();

        }
        protected override void OnResume()
        {
            base.OnResume();
             externalurl = "https://www.adobe.com/content/dam/acom/en/devnet/acrobat/pdfs/pdf_open_parameters.pdf";

            SetWebView(webView, externalurl);

        }
        class MonkeyWebViewClient : WebViewClient
        {
            ImageView _imgViewBack, _imgViewForward, _imgRefresh;
            public MonkeyWebViewClient(ImageView imgViewBack, ImageView imgViewForward, ImageView imgRefresh)
            {
                _imgViewBack = imgViewBack;
                _imgViewForward = imgViewForward;
                _imgRefresh = imgRefresh;
            }
            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {
                base.OnPageStarted(view, url, favicon);
                try
                {
                    _imgRefresh.SetImageResource(Resource.Drawable.close);

                    if (view.CanGoBack())
                    {
                        _imgViewBack.SetImageResource(Resource.Drawable.back_black_arrow);
                    }
                    else
                    {
                        _imgViewBack.SetImageResource(Resource.Drawable.NavArrowLeft);
                    }

                    if (view.CanGoForward())
                    {
                        _imgViewForward.SetImageResource(Resource.Drawable.ArrowRight2);
                    }
                    else
                    {
                        _imgViewForward.SetImageResource(Resource.Drawable.NavArrowRight);
                    }
                    _imgRefresh.Click += (sender, e) => view.StopLoading();
                    //Console.WriteLine ("OnPageStarted");
                }
                catch (Exception)
                {

                }

            }
            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);
                try
                {
                    _imgRefresh.SetImageResource(Resource.Drawable.refresh_icon);
                    if (view.CanGoBack())
                    {
                        _imgViewBack.SetImageResource(Resource.Drawable.back_black_arrow);
                    }
                    else
                    {
                        _imgViewBack.SetImageResource(Resource.Drawable.NavArrowLeft);
                    }

                    if (view.CanGoForward())
                    {
                        _imgViewForward.SetImageResource(Resource.Drawable.ArrowRight2);
                    }
                    else
                    {
                        _imgViewForward.SetImageResource(Resource.Drawable.NavArrowRight);
                    }
                    _imgRefresh.Click += (sender, e) => view.Reload();
                    //Console.WriteLine ("OnPageFinished");
                }
                catch (Exception)
                {

                }
            }
        }

        private void SetWebView(WebView webView, string externalurl)
        {
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.SupportZoom();
            webView.Settings.SetAppCacheEnabled(true);
            webView.Settings.DomStorageEnabled = true;
            webView.ZoomOut();
            webView.ZoomIn();
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.LoadWithOverviewMode = true;
            webView.Settings.UseWideViewPort = true;
            //webview.Settings.SetSupportZoom (true);
            webView.Settings.SetPluginState(WebSettings.PluginState.On);
            webView.Settings.GetPluginState();
            webView.LoadUrl(externalurl);
            webView.SetWebViewClient(new MonkeyWebViewClient(imgViewBack, imgViewForward, imgRefresh));
            webView.SetWebChromeClient(new WebChromeClient());
        }
    
    
    }
}
