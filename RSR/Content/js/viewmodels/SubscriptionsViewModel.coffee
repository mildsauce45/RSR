# CoffeeScript

window.RSR = {} unless window.RSR?

class RSR.SubItemViewModel
    constructor: (sub, parentVM) ->
                
        @id = sub.id
        @name = ko.observable sub.name
        @unreadEntries = ko.observable sub.unreadEntries
        
        @parentVm = parentVM
        
    subscriptionItemClick: () =>
        subId = @id
        promise = $.ajax({
            accepts: 'application/json',
            type: 'GET',
            dataType: 'json',
            url: RSR.webroot + "subscriptions/#{subId}"
        })
        
        promise.then (results) =>
            @parentVm.feedItems.removeAll()
            @parentVm.feedItems.push new RSR.FeedItemViewModel val for val in results
            
class RSR.FeedItemViewModel
    constructor: (fi) ->        
        
        @title = fi.title
        @description = fi.description
        @link = fi.link
        
    feedItemClick: (event) =>
        window.open @link, 'blank'
        event.preventDefault()
        
            
class RSR.SubscriptionsViewModel
    constructor: ->
    
        #Feed Item properties
        @subscriptions = ko.observableArray []
        @feedItems = ko.observableArray []
        
        #Authentication Properties
        @showLoginHeader = ko.observable true
        @showLoginForm = ko.observable false
        @userName = ko.observable ''
        @password = ko.observable ''
        @rememberMe = ko.observable false
        
        @showSubscriptionForm = ko.observable false
        
        @load()
        
    load: () =>
        @userName localStorage?.getItem 'username'
        @password localStorage?.getItem 'password'
        
        savedRemember = localStorage?.getItem 'rememberMe'
        
        #@rememberMe true if savedRemember? and savedRemember is 'true'
        
        #if (@rememberMe())
        #    @showLoginHeader false
        #    @showLoginForm false
            
        #    @getSubscriptions()
        
    login: () =>
        @showLoginForm true
        
    logoff: () =>
        localStorage?.removeItem 'username'
        localStorage?.removeItem 'password'
        localStorage?.removeItem 'rememberMe'
        
        promise = $.ajax { type: 'GET', url: 'logout' }
        
        promise.then (results) =>
            @finishLogoff()
        
    finishLogoff: () =>
        @showLoginHeader true
        @showLoginForm false
            
        @feedItems []
        @subscriptions []
        
    closeDialog: () =>
        @showLoginForm false
        @showSubscriptionForm false
        
    getSubscriptions: () =>
        error = () =>
            @finishLogoff()
            
        promise = $.ajax({
            accepts: 'application/json',
            type: 'GET',
            dataType: 'json',
            url: RSR.webroot + 'subscriptions',
            error: error
        })
        
        promise.then (results) =>
            @subscriptions.push new RSR.SubItemViewModel val, @ for val in results
            
            if (@subscriptions().length > 0)
                @subscriptions()[0].subscriptionItemClick()
        
    submitCredentials: () =>
        credentials = { userName: @userName(), password: @password(), rememberMe: @rememberMe() }               
        
        promise = $.ajax({
            accepts: 'application/json',
            type: 'POST',
            data: credentials,
            dataType: 'json',
            url: RSR.webroot + 'login'
        })
        
        promise.then (results) =>            
            
            if (results.success)
                @showLoginHeader false
                @showLoginForm false
                
                localStorage?.setItem 'username', @userName()
                localStorage?.setItem 'password', @password()
                localStorage?.setItem 'rememberMe', @rememberMe()
                
                $.cookie '_ncfa', results.cookie
                
                @getSubscriptions()
            else
                localStorage?.removeItem 'username'
                localStorage?.removeItem 'password'
                localStorage?.removeItem 'rememberMe'
                
    addSubscription: () =>
        @showSubscriptionForm true