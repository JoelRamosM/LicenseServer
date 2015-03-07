
function LicensesViewModel() {
    var self = this;
    var url = location.origin;

    var testArray = [new LicenseModel({ Id: 1, Produto: "Teste 1", Empresa: "Empresa 1", AppKey: "1" }),
                     new LicenseModel({ Id: 2, Produto: "Teste 2", Empresa: "Empresa 2", AppKey: "2" }),
                     new LicenseModel({ Id: 3, Produto: "Teste 3", Empresa: "Empresa 3", AppKey: "3" }),
                     new LicenseModel({ Id: 4, Produto: "Teste 4", Empresa: "Empresa 4", AppKey: "4" }), ];
    this.Licenses = ko.observableArray();
    this.SelectedLicense = ko.observable();

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
    this.Id = ko.observable(data.Id);
    this.Produto = ko.observable(data.Produto);
    this.Empresa = ko.observable(data.Empresa);
    this.AppKey = ko.observable(data.AppKey);
    this.selected = ko.observable(false);

    this.onSelect = function () {
        self.selected(!self.selected());
        ko.postbox.publish("licenseSelected", ko.toJS(self));
    }

}