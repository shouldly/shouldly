//-------------------------------------------------------------------------------------------------
// <copyright file="rssutil.h" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
//    
//    The use and distribution terms for this software are covered by the
//    Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)
//    which can be found in the file CPL.TXT at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//    
//    You must not remove this notice, or any other, from this software.
// </copyright>
// 
// <summary>
//    RSS helper funtions.
// </summary>
//-------------------------------------------------------------------------------------------------

#pragma once


#ifdef __cplusplus
extern "C" {
#endif

#define ReleaseRssChannel(p) if (p) { RssFreeChannel(p); }
#define ReleaseNullRssChannel(p) if (p) { RssFreeChannel(p); p = NULL; }


struct RSS_UNKNOWN_ATTRIBUTE
{
    LPWSTR wzNamespace;
    LPWSTR wzAttribute;
    LPWSTR wzValue;

    RSS_UNKNOWN_ATTRIBUTE* pNext;
};

struct RSS_UNKNOWN_ELEMENT
{
    LPWSTR wzNamespace;
    LPWSTR wzElement;
    LPWSTR wzValue;

    RSS_UNKNOWN_ATTRIBUTE* pAttributes;
    RSS_UNKNOWN_ELEMENT* pNext;
};

struct RSS_ITEM
{
    LPWSTR wzTitle;
    LPWSTR wzLink;
    LPWSTR wzDescription;

    LPWSTR wzGuid;
    FILETIME ftPublished;

    LPWSTR wzEnclosureUrl;
    DWORD dwEnclosureSize;
    LPWSTR wzEnclosureType;

    RSS_UNKNOWN_ELEMENT* pUnknownElements;
};

struct RSS_CHANNEL
{
    LPWSTR wzTitle;
    LPWSTR wzLink;
    LPWSTR wzDescription;
    DWORD dwTimeToLive;

    RSS_UNKNOWN_ELEMENT* pUnknownElements;

    DWORD cItems;
    RSS_ITEM rgItems[1];
};

HRESULT DAPI RssInitialize(
    );

void DAPI RssUninitialize(
    );

HRESULT DAPI RssParseFromString(
    __in_z LPCWSTR wzRssString,
    __out RSS_CHANNEL **ppChannel
    );

HRESULT DAPI RssParseFromFile(
    __in_z LPCWSTR wzRssFile,
    __out RSS_CHANNEL **ppChannel
    );

// Adding this until we have the updated specstrings.h
#ifndef __in_xcount
#define __in_xcount(size) 
#endif

void DAPI RssFreeChannel(
    __in_xcount(pChannel->cItems) RSS_CHANNEL *pChannel
    );

#ifdef __cplusplus
}
#endif

