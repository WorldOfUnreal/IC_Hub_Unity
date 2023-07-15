mergeInto(LibraryManager.library, {
  // Create a new function with the same name as
  // the event listeners name and make sure the
  // parameters match as well.

  JSLogin: function () {
    ReactUnityWebGL.Login();
  },

  JSCreateUser: function (username) {
    ReactUnityWebGL.CreateUser(Pointer_stringify(username));
    ///ReactUnityWebGL.SavePlayerConfig(Pointer_stringify(json));
  },

  JSSelectChatGroup: function (idChat) {
    ReactUnityWebGL.SelectChatGroup(idChat);
  },

  JSSendMessage: function (message) {
    ReactUnityWebGL.SendMessage(Pointer_stringify(message));
  },

  JSAddUserToGroup: function (json) {
    ReactUnityWebGL.AddUserToGroup(Pointer_stringify(json));
  },

  JSCreateGroup: function (json) {
    ReactUnityWebGL.CreateGroup(Pointer_stringify(json));
  },
  
  JSSearchGroup: function (searchtext) {
    ReactUnityWebGL.SearchGroup(Pointer_stringify(searchtext));
  },
      
  JSSearchUser: function (searchtext) {
    ReactUnityWebGL.SearchUser(Pointer_stringify(searchtext));
  },
        
  JSRequestJoinGroup: function (idGroup) {
    ReactUnityWebGL.RequestJoinGroup(idGroup);
  },
  
  JSLeaveGroup: function (idChat) {
    ReactUnityWebGL.LeaveGroup(idChat);
  },
  
  JSAcceptRequest: function (json) {
    ReactUnityWebGL.AcceptRequest(Pointer_stringify(json));
  },
  
  JSDenyRequest: function (json) {
    ReactUnityWebGL.DenyRequest(Pointer_stringify(json));
  },
  
  JSKickUser: function (json) {
    ReactUnityWebGL.KickUser(Pointer_stringify(json));
  },
  
  JSMakeAdmin: function (json) {
    ReactUnityWebGL.MakeAdmin(Pointer_stringify(json));
  },
  
  JSRemoveAdmin: function (json) {
    ReactUnityWebGL.RemoveAdmin(Pointer_stringify(json));
  },
  
  JSSetGroupPrivate: function (idGroup) {
    ReactUnityWebGL.SetGroupPrivate(idGroup);
  },
  
  JSSetGroupPublic: function (idGroup) {
    ReactUnityWebGL.SetGroupPublic(idGroup);
  },
  
  JSChangeTitle: function (json) {
    ReactUnityWebGL.ChangeTitle(Pointer_stringify(json));
  },
    
  JSChangeDescription: function (json) {
    ReactUnityWebGL.ChangeDescription(Pointer_stringify(json));
  },
  
  JSChangeDescriptionUser: function (text) {
    ReactUnityWebGL.ChangeDescriptionUser(Pointer_stringify(text));
  },
  
  JSCallToUser: function (principalID) {
    ReactUnityWebGL.CallToUser(Pointer_stringify(principalID));
  },
  
  JSAcceptFriendRequest: function (principalID) {
    ReactUnityWebGL.AcceptFriendRequest(Pointer_stringify(principalID));
  },
  
  JSDenyFriendRequest: function (principalID) {
    ReactUnityWebGL.DenyFriendRequest(Pointer_stringify(principalID));
  },
  
  JSSendFriendRequest: function (principalID) {
    ReactUnityWebGL.SentFriendRequest(Pointer_stringify(principalID));
  },
  
  JSSendMessageToUser: function (principalID) {
    ReactUnityWebGL.SendMessageToUser(Pointer_stringify(principalID));
  },
  
  JSLogoutFromProfile: function () {
    ReactUnityWebGL.LogoutFromProfile();
  },
  
  JSChangeStatus: function (state) {
      ReactUnityWebGL.ChangeStatus(state);
    },
    
  JSSendDataApp: function (json) {
      ReactUnityWebGL.SendDataApp(Pointer_stringify(json));
    },
    
  JSSendDataVersions: function (json) {
      ReactUnityWebGL.SendDataVersions(Pointer_stringify(json));
    },
    
  JSSendDataCollections: function (json) {
      ReactUnityWebGL.SendDataCollections(Pointer_stringify(json));
    },
        
  JSDeleteVersion: function (versionID) {
      ReactUnityWebGL.DeleteVersion(versionID);
    },
    
  JSDeleteCollection: function (versionID) {
      ReactUnityWebGL.DeleteCollection(versionID);
    },
    
  JSSendDataNews: function (json) {
      ReactUnityWebGL.SendDataNews(Pointer_stringify(json));
    },
    
  JSSetImage: function (text) {
      ReactUnityWebGL.SetImage(Pointer_stringify(text));
    },
    
  JSCurrentSection: function (id) {
      ReactUnityWebGL.CurrentSection(id);
    },
    
  JSSendCrypto: function (json) {
      ReactUnityWebGL.SendCrypto(Pointer_stringify(json));
    },
    
  JSSendReport: function (json) {
      ReactUnityWebGL.SendReport(Pointer_stringify(json));
    },
    
  JSAddAppToFavorite: function (id) {
      ReactUnityWebGL.AddAppToFavorite(id);
    },
    
    
    
    
});