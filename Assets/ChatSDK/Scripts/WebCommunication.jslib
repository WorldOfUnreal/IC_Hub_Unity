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

  JSCreateGroup: function (groupname) {
    ReactUnityWebGL.CreateGroup(Pointer_stringify(groupname));
  },
  
  JSSearchGroup: function (searchtext) {
    ReactUnityWebGL.SearchGroup(Pointer_stringify(searchtext));
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
  
});