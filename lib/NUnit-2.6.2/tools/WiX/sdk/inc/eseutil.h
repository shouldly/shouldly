#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="eseutil.h" company="Microsoft">
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
//    Header for Extensible Storage Engine (Jetblue) helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

struct COLUMN_SCHEMA
{
    JET_COLUMNID jcColumn;
    LPCWSTR pszName;
    JET_COLTYP jcColumnType;
    BOOL fKey; // If this column is part of the key of the table
    BOOL fFixed;
    BOOL fNullable;
    BOOL fAutoIncrement;
};

struct TABLE_SCHEMA
{
    JET_TABLEID jtTable;
    LPCWSTR pszName;
    DWORD dwColumns;
    COLUMN_SCHEMA *pcsColumns;
};

struct DATABASE_SCHEMA
{
    DWORD dwTables;
    TABLE_SCHEMA *ptsTables;
};

typedef void* ESE_QUERY_HANDLE;

HRESULT DAPI EseBeginSession(
    __out JET_INSTANCE *pjiInstance,
    __out JET_SESID *pjsSession,
    __in_z LPCWSTR pszInstance,
    __in_z LPCWSTR pszPath
    );
HRESULT DAPI EseEndSession(
    __in JET_INSTANCE jiInstance,
    __in JET_SESID jsSession
    );
HRESULT DAPI EseEnsureDatabase(
    __in JET_SESID jsSession,
    __in_z LPCWSTR pszFile,
    __in DATABASE_SCHEMA *pdsSchema,
    __out JET_DBID* pjdbDb,
    __in BOOL fExclusive,
    __in BOOL fReadonly
    );
HRESULT DAPI EseCloseDatabase(
    __in JET_SESID jsSession,
    __in JET_DBID jdbDb
    );
HRESULT DAPI EseCreateTable(
    __in JET_SESID jsSession,
    __in JET_DBID jdbDb,
    __in_z LPCWSTR pszTable,
    __out JET_TABLEID *pjtTable
    );
HRESULT DAPI EseOpenTable(
    __in JET_SESID jsSession,
    __in JET_DBID jdbDb,
    __in_z LPCWSTR pszTable,
    __out JET_TABLEID *pjtTable
    );
HRESULT DAPI EseCloseTable(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable
    );
HRESULT DAPI EseEnsureColumn(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in_z LPCWSTR pszColumnName,
    __in JET_COLTYP jcColumnType,
    __in ULONG ulColumnSize,
    __in BOOL fFixed,
    __in BOOL fNullable,
    __out_opt JET_COLUMNID *pjcColumn
    );
HRESULT DAPI EseGetColumn(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in_z LPCWSTR pszColumnName,
    __out JET_COLUMNID *pjcColumn
    );
HRESULT DAPI EseMoveCursor(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in LONG lRow
    );
HRESULT DAPI EseDeleteRow(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable
    );
HRESULT DAPI EseBeginTransaction(
    __in JET_SESID jsSession
    );
HRESULT DAPI EseCommitTransaction(
    __in JET_SESID jsSession
    );
HRESULT DAPI EsePrepareUpdate(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in ULONG ulPrep
    );
HRESULT DAPI EseFinishUpdate(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable
    );
HRESULT DAPI EseSetColumnDword(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __in DWORD dwValue
    );
// Sets a column value without the need to call begintransaction, prepareupdate, finishupdate, or committransaction (all of these are called in sequence for you)
HRESULT DAPI EseSetColumnDwordFull(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __in DWORD dwValue
    );
HRESULT DAPI EseSetColumnString(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __in_z LPCWSTR pszValue
    );
// Sets a column value without the need to call begintransaction, prepareupdate, finishupdate, or committransaction (all of these are called in sequence for you)
HRESULT DAPI EseSetColumnStringFull(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __in_z LPCWSTR pszValue
    );
HRESULT DAPI EseSetColumnEmpty(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn
    );
HRESULT DAPI EseSetColumnEmptyFull(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn
    );
HRESULT DAPI EseGetColumnDword(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __out DWORD *pdwValue
    );
HRESULT DAPI EseGetColumnString(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in JET_COLUMNID jcColumn,
    __out LPWSTR *ppszValue
    );

// Call this once for each key column in the table
HRESULT DAPI EseBeginQuery(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in BOOL fExact,
    __out ESE_QUERY_HANDLE *peqhHandle
    );
HRESULT DAPI EseSetQueryColumnDword(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in ESE_QUERY_HANDLE eqhHandle,
    __in DWORD dwData,
    __in BOOL fFinal // If this is true, all other key columns in the query will be set to "*"
    );
HRESULT DAPI EseSetQueryColumnString(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in ESE_QUERY_HANDLE eqhHandle,
    __in_z LPCWSTR pszString,
    __in BOOL fFinal // If this is true, all other key columns in the query will be set to "*"
    );
// This frees the query handle without actually running the query
void DAPI EseFreeQuery(
    __in ESE_QUERY_HANDLE eqhHandle
    );
// Once all columns have been set up, call this and read the result
HRESULT DAPI EseRunQuery(
    __in JET_SESID jsSession,
    __in JET_TABLEID jtTable,
    __in ESE_QUERY_HANDLE eqhHandle
    );

#ifdef __cplusplus
}
#endif
