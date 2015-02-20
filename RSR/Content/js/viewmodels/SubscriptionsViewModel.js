(function() {
  var __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  if (window.RSR == null) {
    window.RSR = {};
  }

  RSR.SubItemViewModel = (function() {
    function SubItemViewModel(sub, parentVM) {
      this.subscriptionItemClick = __bind(this.subscriptionItemClick, this);
      this.id = sub.id;
      this.name = ko.observable(sub.name);
      this.unreadEntries = ko.observable(sub.unreadEntries);
      this.parentVm = parentVM;
    }

    SubItemViewModel.prototype.subscriptionItemClick = function() {
      var promise, subId;
      subId = this.id;
      promise = $.ajax({
        accepts: 'application/json',
        type: 'GET',
        dataType: 'json',
        url: "RSR/subscriptions/" + subId
      });
      return promise.then((function(_this) {
        return function(results) {
          var val, _i, _len, _results;
          _this.parentVm.feedItems.removeAll();
          _results = [];
          for (_i = 0, _len = results.length; _i < _len; _i++) {
            val = results[_i];
            _results.push(_this.parentVm.feedItems.push(new RSR.FeedItemViewModel(val)));
          }
          return _results;
        };
      })(this));
    };

    return SubItemViewModel;

  })();

  RSR.FeedItemViewModel = (function() {
    function FeedItemViewModel(fi) {
      this.feedItemClick = __bind(this.feedItemClick, this);
      this.title = fi.title;
      this.description = fi.description;
      this.link = fi.link;
    }

    FeedItemViewModel.prototype.feedItemClick = function(event) {
      window.open(this.link, 'blank');
      return event.preventDefault();
    };

    return FeedItemViewModel;

  })();

  RSR.SubscriptionsViewModel = (function() {
    function SubscriptionsViewModel() {
      this.addSubscription = __bind(this.addSubscription, this);
      this.submitCredentials = __bind(this.submitCredentials, this);
      this.getSubscriptions = __bind(this.getSubscriptions, this);
      this.closeDialog = __bind(this.closeDialog, this);
      this.finishLogoff = __bind(this.finishLogoff, this);
      this.logoff = __bind(this.logoff, this);
      this.login = __bind(this.login, this);
      this.load = __bind(this.load, this);
      this.subscriptions = ko.observableArray([]);
      this.feedItems = ko.observableArray([]);
      this.showLoginHeader = ko.observable(true);
      this.showLoginForm = ko.observable(false);
      this.userName = ko.observable('');
      this.password = ko.observable('');
      this.rememberMe = ko.observable(false);
      this.showSubscriptionForm = ko.observable(false);
      this.load();
    }

    SubscriptionsViewModel.prototype.load = function() {
      var savedRemember;
      this.userName(typeof localStorage !== "undefined" && localStorage !== null ? localStorage.getItem('username') : void 0);
      this.password(typeof localStorage !== "undefined" && localStorage !== null ? localStorage.getItem('password') : void 0);
      return savedRemember = typeof localStorage !== "undefined" && localStorage !== null ? localStorage.getItem('rememberMe') : void 0;
    };

    SubscriptionsViewModel.prototype.login = function() {
      return this.showLoginForm(true);
    };

    SubscriptionsViewModel.prototype.logoff = function() {
      var promise;
      if (typeof localStorage !== "undefined" && localStorage !== null) {
        localStorage.removeItem('username');
      }
      if (typeof localStorage !== "undefined" && localStorage !== null) {
        localStorage.removeItem('password');
      }
      if (typeof localStorage !== "undefined" && localStorage !== null) {
        localStorage.removeItem('rememberMe');
      }
      promise = $.ajax({
        type: 'GET',
        url: 'logout'
      });
      return promise.then((function(_this) {
        return function(results) {
          return _this.finishLogoff();
        };
      })(this));
    };

    SubscriptionsViewModel.prototype.finishLogoff = function() {
      this.showLoginHeader(true);
      this.showLoginForm(false);
      this.feedItems([]);
      return this.subscriptions([]);
    };

    SubscriptionsViewModel.prototype.closeDialog = function() {
      this.showLoginForm(false);
      return this.showSubscriptionForm(false);
    };

    SubscriptionsViewModel.prototype.getSubscriptions = function() {
      var error, promise;
      error = (function(_this) {
        return function() {
          return _this.finishLogoff();
        };
      })(this);
      promise = $.ajax({
        accepts: 'application/json',
        type: 'GET',
        dataType: 'json',
        url: 'RSR/subscriptions',
        error: error
      });
      return promise.then((function(_this) {
        return function(results) {
          var val, _i, _len;
          for (_i = 0, _len = results.length; _i < _len; _i++) {
            val = results[_i];
            _this.subscriptions.push(new RSR.SubItemViewModel(val, _this));
          }
          if (_this.subscriptions().length > 0) {
            return _this.subscriptions()[0].subscriptionItemClick();
          }
        };
      })(this));
    };

    SubscriptionsViewModel.prototype.submitCredentials = function() {
      var credentials, promise;
      credentials = {
        userName: this.userName(),
        password: this.password(),
        rememberMe: this.rememberMe()
      };
      promise = $.ajax({
        accepts: 'application/json',
        type: 'POST',
        data: credentials,
        dataType: 'json',
        url: 'RSR/login'
      });
      return promise.then((function(_this) {
        return function(results) {
          if (results.success) {
            _this.showLoginHeader(false);
            _this.showLoginForm(false);
            if (typeof localStorage !== "undefined" && localStorage !== null) {
              localStorage.setItem('username', _this.userName());
            }
            if (typeof localStorage !== "undefined" && localStorage !== null) {
              localStorage.setItem('password', _this.password());
            }
            if (typeof localStorage !== "undefined" && localStorage !== null) {
              localStorage.setItem('rememberMe', _this.rememberMe());
            }
            $.cookie('_ncfa', results.cookie);
            return _this.getSubscriptions();
          } else {
            if (typeof localStorage !== "undefined" && localStorage !== null) {
              localStorage.removeItem('username');
            }
            if (typeof localStorage !== "undefined" && localStorage !== null) {
              localStorage.removeItem('password');
            }
            return typeof localStorage !== "undefined" && localStorage !== null ? localStorage.removeItem('rememberMe') : void 0;
          }
        };
      })(this));
    };

    SubscriptionsViewModel.prototype.addSubscription = function() {
      return this.showSubscriptionForm(true);
    };

    return SubscriptionsViewModel;

  })();

}).call(this);

//# sourceMappingURL=SubscriptionsViewModel.js.map
