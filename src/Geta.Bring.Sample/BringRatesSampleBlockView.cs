using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Geta.Bring.Shipping.Model;

namespace Geta.Bring.Sample
{
    public class BringRatesSampleBlockView
    {
        public ViewState State { get; }

        [Required]
        [DisplayName("Fra land")]
        public string CountryFrom { get; set; }

        [Required(ErrorMessage = "Fra postnummer er påkrevd")]
        [DisplayName("Fra postnummer *")]
        public string PostalCodeFrom { get; set; }

        [Required]
        [DisplayName("Til land")]
        public string CountryTo { get; set; }

        [DisplayName("Til postnummer *")]
        [Required(ErrorMessage = "Til postnummer er påkrevd")]
        public string PostalCodeTo { get; set; }

        [DisplayName("Forsendelsesdato")]
        public DateTime? ShippingDateAndTime { get; set; }

        [DisplayName("Vekt (gram)")]
        public int? Weight { get; set; }

        [DisplayName("Lengde (cm)")]
        public int? Length { get; set; }

        [DisplayName("Bredde (cm)")]
        public int? Width { get; set; }

        [DisplayName("Høyde (cm)")]
        public int? Height { get; set; }

        [DisplayName("Volum (liter/dm³)")]
        public int? Volume { get; set; }
        
        [DisplayName("EDI")]
        public bool Edi { get; set; }

        [DisplayName("Sendes fra postkontoret")]
        public bool ShippedFromPostOffice { get; set; }

        [DisplayName("Produkter")]
        public string[] Products { get; set; }

        [DisplayName("Tilleggstjenester")]
        public string[] AdditionalServices { get; set; }

        public IEnumerable<EstimateGroup> EstimateGroups { get; }

        public IEnumerable<SelectListItem> CountriesFromList
        {
            get
            {
                yield return new SelectListItem { Value = "NO", Text = "Norway" };
                yield return new SelectListItem { Value = "SE", Text = "Sweden" };
                yield return new SelectListItem { Value = "FI", Text = "Finland" };
                yield return new SelectListItem { Value = "DK", Text = "Denmark" };
            }
        }

        public IEnumerable<SelectListItem> CountriesToList
        {
            get
            {
                yield return new SelectListItem { Value = "NO", Text = "Norway" };
                yield return new SelectListItem { Value = "SE", Text = "Sweden" };
                yield return new SelectListItem { Value = "FI", Text = "Finland" };
                yield return new SelectListItem { Value = "DK", Text = "Denmark" };
            }
        }

        public IEnumerable<SelectListItem> ProductList
        {
            get { return Product.All.Select(x => new SelectListItem { Value = x.Code, Text = x.DisplayName }); }
        }

        public IEnumerable<SelectListItem> AdditionalServiceList
        {
            get { return AdditionalService.All.Select(x => new SelectListItem { Value = x.Code, Text = x.DisplayName }); }
        }

        public BringRatesSampleBlockView()
        {
            State = ViewState.SearchForm;
            EstimateGroups = Enumerable.Empty<EstimateGroup>();
        }

        public BringRatesSampleBlockView(IEnumerable<EstimateGroup> estimateGroups)
        {
            State = ViewState.Results;
            EstimateGroups = estimateGroups;
        }

        public class EstimateGroup
        {
            public string MainCategory { get; }
            public IEnumerable<ShipmentEstimate> Estimates { get; }

            public EstimateGroup(string mainCategory, IEnumerable<ShipmentEstimate> estimates)
            {
                MainCategory = mainCategory ?? throw new ArgumentNullException(nameof(mainCategory));
                Estimates = estimates ?? throw new ArgumentNullException(nameof(estimates));
            }
        }

        public enum ViewState
        {
            SearchForm,
            Results
        }
    }
}