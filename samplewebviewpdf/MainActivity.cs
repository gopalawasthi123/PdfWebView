using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace samplewebviewpdf
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button openwebviewbutton;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            openwebviewbutton = FindViewById<Button>(Resource.Id.btn);
            openwebviewbutton.Text = "OpenPdf";
            openwebviewbutton.Click += Openwebviewbutton_Click;
        }

        private void Openwebviewbutton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this,typeof(WebViewPdf));
            StartActivity(intent);
        }
    }
}