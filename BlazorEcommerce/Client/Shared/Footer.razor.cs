using WMBlazorSlickCarousel.WMBSC;

namespace BlazorEcommerce.Client.Shared
{
    public partial class Footer
    {
        public BlazorSlickCarousel theCarousel;
        public WMBSCInitialSettings configurations;

        protected override void OnInitialized()
        {
            // to <= 992px screen
            WMBSCSettings breakpoint992Settings = new WMBSCSettings
            {
                arrows = false,
                slidesToShow = 4,
            };
            WMBSCResponsiveSettings breakpoint992Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 992,
                settings = breakpoint992Settings
            };

            // to <= 768px screen
            WMBSCSettings breakpoint768Settings = new WMBSCSettings
            {
                arrows = false,
                slidesToShow = 3
            };
            WMBSCResponsiveSettings breakpoint768Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 768,
                settings = breakpoint768Settings
            };

            // to <= 575px screen
            WMBSCSettings breakpoint575Settings = new WMBSCSettings
            {
                arrows = false,
                slidesToShow = 3
            };
            WMBSCResponsiveSettings breakpoint575Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 575,
                settings = breakpoint575Settings
            };

            // to <= 480px screen
            WMBSCSettings breakpoint480Settings = new WMBSCSettings
            {
                arrows = false,
                slidesToShow = 2
            };
            WMBSCResponsiveSettings breakpoint480Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 480,
                settings = breakpoint480Settings
            };

            // to <= 320px screen
            WMBSCSettings breakpoint320Settings = new WMBSCSettings
            {
                arrows = false,
                slidesToShow = 1
            };
            WMBSCResponsiveSettings breakpoint320Responsive = new WMBSCResponsiveSettings
            {
                breakpoint = 320,
                settings = breakpoint320Settings
            };

            // add responsivity
            List<WMBSCResponsiveSettings> responsiveSettingsList = new List<WMBSCResponsiveSettings>();
            responsiveSettingsList.Add(breakpoint992Responsive);
            responsiveSettingsList.Add(breakpoint768Responsive);
            responsiveSettingsList.Add(breakpoint575Responsive);
            responsiveSettingsList.Add(breakpoint480Responsive);
            responsiveSettingsList.Add(breakpoint320Responsive);

            configurations = new WMBSCInitialSettings
            {
                arrows = false,
                autoplay = true,
                autoplaySpeed = 8000,
                slidesToShow = 6,
                infinite = false,
                responsive = responsiveSettingsList
            };
        }
    }
}
