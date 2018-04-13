using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using CricketScoreSheetPro.Droid.Activity;
using System;
using System.IO;

namespace CricketScoreSheetPro.Droid
{
    [Activity(Label = "Cricket Score Sheet", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/ic_launcher"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        private ActionBarDrawerToggle _drawerToggle;
        private DrawerLayout _drawerLayout;

        protected override int GetLayoutResourceId => Resource.Layout.Main;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.left_drawer);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);            
            _drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, Toolbar,
                Resource.String.ApplicationName, Resource.String.ApplicationName);
            _drawerLayout.AddDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();

            var prevFragment = FragmentManager.FindFragmentById(Resource.Id.content_frame);
            if (prevFragment == null)
            {
                var ft = FragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, new HomeFragment());
                ft.Commit();
            }

            //Create unique Id and store in database
            var uiniqueUserIdfile = Path.Combine(Helper.InternalPath, "UniqueUserId.txt");
            if (File.Exists(uiniqueUserIdfile))
            {
                var existingGuid = File.ReadAllText(uiniqueUserIdfile);
                Driver.UniqueUserId = existingGuid;
            }
            else
            {
                var newGuid = Guid.NewGuid().ToString();
                Driver.UniqueUserId = newGuid;
                File.WriteAllText(uiniqueUserIdfile, newGuid);
            }            
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    SupportActionBar.SetTitle(Resource.String.ApplicationName);
                    ft.Replace(Resource.Id.content_frame, new HomeFragment(), nameof(HomeFragment));
                    FragmentManager.PopBackStackImmediate(null, PopBackStackFlags.Inclusive);
                    break;
                case (Resource.Id.nav_tournaments):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break;
				case (Resource.Id.nav_teams):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TeamFragment(), nameof(TeamFragment));
                    break;
                case (Resource.Id.nav_matches):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break;
				case (Resource.Id.nav_teamstatistics):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break; 
                case (Resource.Id.nav_batsmanstatistics):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break; 
                case (Resource.Id.nav_bowlerstatistics):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break; 
                case (Resource.Id.nav_fielderstatistics):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentFragment(), nameof(TournamentFragment));
                    break; 
            }
            ft.Commit();
            _drawerLayout.CloseDrawers();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.FindItem(Resource.Id.searchText).SetVisible(false);
            _drawerToggle.DrawerIndicatorEnabled = true;
            var currentfragment = FragmentManager.FindFragmentById(Resource.Id.content_frame);
            switch (currentfragment.Tag)
            {
                case nameof(TournamentFragment):
                    SupportActionBar.SetTitle(Resource.String.TournamentFragment);
                    menu.FindItem(Resource.Id.searchText).SetVisible(true);
                    break;
                case nameof(TeamFragment):
                    SupportActionBar.SetTitle(Resource.String.TeamFragment);
                    menu.FindItem(Resource.Id.searchText).SetVisible(true);
                    break;
            }
            return base.OnPrepareOptionsMenu(menu);
        }
       
    }
}

