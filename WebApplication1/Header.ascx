<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="WebApplication1.Header" %>
<div data-role="header">
                <div data-role="popup" class="popupMenu" data-theme="a">
                    <a href="#" data-theme="b" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn-a ui-icon-delete ui-btn-icon-notext ui-btn-left">Close</a>
                </div>
                <div class="ui-grid-a centeredText">
                    <div class=" ui-block-a" style="width:20%">
                        <div class="logo"></div>
                    </div>
                    <div class="ui-block-b rightText" style="width:80%">
                        <a href="#mypanel" data-rel="popup" data-transition="slidedown" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-icon-bars  ui-btn-a ui-btn-icon-notext panelBtn">Actions...</a>
                        <div class="notificationBox">
                            <div class="notificationContainer ">
                                <input type="button" data-icon="delete" data-iconpos="notext" value="Icon only" />
                                <div class="notificationNumber">
                                    1
                                </div>
                            </div>
                            <div class="notificationContainer ">
                                <input type="button" data-icon="delete" data-iconpos="notext" value="Icon only" />
                                <div class="notificationNumber">
                                    2
                                </div>
                            </div>
                            <div class="notificationContainer ">
                                <input type="button" data-icon="delete" data-iconpos="notext" value="Icon only" />
                                <div class="notificationNumber">
                                    3
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>