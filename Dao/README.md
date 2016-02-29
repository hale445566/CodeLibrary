#API

[TOC]


##SqlAccess
MySql数据库接入，增删改查。
###Variables
| 变量名 | 说明 |
|--------|--------|
|  host  |数据库ip地址|
|id		|数据库用户id|
|pwd	|数据库密码|
|database|要打开的数据库名|

###Public Functions
|方法名|参数|返回值|说明|
|---|---|---|---|
|OpenSql|null|void|打开数据库,数据库ip，id，密码，数据库名为变量中设置的对应参数|
|CreateTable|**string** name, **string[]** col,**string[]** colType|DataSet|创建一个新表，name-表名，col-值，colType-值类型|
|CreateTableAutoID|**string** name,**string[]** col,**string[]** colType|DataSet|创建一个带id的表，name-表名，col-值，colType-值类型|
|InsertInto|**string** tableName,**string[]** values|DataSet|在数据库中插入一整条数据，不适用自动累加id。tableName-表名，values-每列的值|
|InsertInto|**string** tableName,**string[]** col,**string[]** values|DataSet|在数据库中插入部分数据。tableName-表名，col-列名,values-列值|
|SelectWhere|**string** tableName,**string[]** items,**string[]** col,**string[]** operation,**string[]** values|DataSet|查询数据。tableName-表名,items-要查询的数据名,col-列名,operation-逻辑判断符,values-列值|
|UpdateInto|**string** tableName,**string[]** cols,**string[]** colsvalues,**string** selectkey,**string** selectvalue|DataSet|修改数据。tableName-表名,cols-列名，colsvalues-列值，selectkey-选中的列名,selectvalue-列值|
|Delete|**string** tableName,**string[]** cols,**string[]** colsvalues|DataSet|删除数据。tableName-表名，cols-列名，colsvalues-列值|

