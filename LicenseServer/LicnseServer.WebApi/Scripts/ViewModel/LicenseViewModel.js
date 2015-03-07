
function LicensesViewModel() {
    var self = this;
    var url = location.origin;

    this.Licenses = ko.observableArray();
    this.SelectedLicense = ko.observable();

    this.novaLicense = ko.observable(new LicenseModel({}));
    this.showNovo = ko.observable(false);
    this.onNovo = function () {
        self.showNovo(true);

    }
    this.closeNovo = function () {
        self.showNovo(false);
    }

    ko.postbox.subscribe("licenseSelected", function (value) {
        self.SelectedLicense(value);
        self.Licenses().forEach(function (data) {
            if (data.Id() == self.SelectedLicense().Id)
                return;
            data.selected(false);
        });
    });

    this.getLicenses = function () {
        $.ajax(url + "/api/license/inicio", {
            success: function (data) {
                data.forEach(function (license) {
                    self.Licenses.push(new LicenseModel(license));
                });
            }
        })
            .done(function (data) {
            });
    }
}

function LicenseModel(data) {
    var self = this;
    this.Id = ko.observable(data.Id || "");
    this.Produto = ko.observable(data.Produto || "");
    this.Empresa = ko.observable(data.Empresa || "");
    this.AppKey = ko.observable(data.AppKey || "");
    this.selected = ko.observable(false);

    this.onSelect = function () {
        self.selected(!self.selected());
        ko.postbox.publish("licenseSelected", ko.toJS(self));
    }

}