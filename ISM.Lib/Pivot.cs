using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace ISM.Lib
{
    public class Pivot
    {
        #region Sub Classes

        public enum PlaceOfTotalType
        {
            Left = 0,
            Right = 1,
        }
        public enum AggregateType
        {
            None = 0,
            Count,
            Sum,
            Min,
            Max,
            Avg,
            Last,
            First
        }
        public class FieldInfo
        {
            public string FieldName;	//source table field name
            public string FieldAlias;	//destination table field name
            public AggregateType Aggregate;
            public bool ShowTotal;
            protected internal string NewFieldName;

            public float Width = 1;
            public float Height = 0.5f;
        }

        public class Field
        {
            #region Member Variables

            public ArrayList Fields = new ArrayList();

            #endregion

            #region Member Methods

            public void Add(string FieldName, string FieldAlias, float width, float height, AggregateType Aggregate, bool ShowTotal)
            {
                if (!(string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldAlias)))
                {
                    FieldInfo Field = new FieldInfo();
                    Field.FieldName = FieldName;
                    Field.FieldAlias = FieldAlias;
                    Field.Aggregate = Aggregate;
                    Field.ShowTotal = ShowTotal;
                    Field.Width = width;
                    Field.Height = height;
                    Fields.Add(Field);
                }
            }
            public void Add(string FieldName, string FieldAlias, float width, float height, bool ShowTotal)
            {
                if (!(string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldAlias)))
                {
                    FieldInfo Field = new FieldInfo();
                    Field.FieldName = FieldName;
                    Field.FieldAlias = FieldAlias;
                    Field.ShowTotal = ShowTotal;
                    Field.Aggregate = AggregateType.None;
                    Field.Width = width;
                    Field.Height = height;
                    Fields.Add(Field);
                }
            }
            public void RemoveAt(int Index)
            {
                if (Index > -1 && Index < Fields.Count)
                    Fields.RemoveAt(Index);
            }
            public void Remove(string FieldAlias)
            {
                foreach (FieldInfo field in Fields)
                {
                    if (field.FieldAlias == FieldAlias)
                    { Fields.Remove(field); break; }
                }
            }
            public void Insert(int Index, string FieldName, string FieldAlias, float width, float height, AggregateType Aggregate, bool ShowTotal)
            {
                if (!(string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldAlias)))
                {
                    FieldInfo Field = new FieldInfo();
                    Field.FieldName = FieldName;
                    Field.FieldAlias = FieldAlias;
                    Field.Aggregate = Aggregate;
                    Field.ShowTotal = ShowTotal;
                    Field.Width = width;
                    Field.Height = height;
                    Fields.Insert(Index, Field);
                }
            }
            public void Insert(int Index, string FieldName, string FieldAlias, float width, float height, bool ShowTotal)
            {
                if (!(string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldAlias)))
                {
                    FieldInfo Field = new FieldInfo();
                    Field.FieldName = FieldName;
                    Field.FieldAlias = FieldAlias;
                    Field.Aggregate = AggregateType.None;
                    Field.ShowTotal = ShowTotal;
                    Field.Width = width;
                    Field.Height = height;
                    Fields.Insert(Index, Field);
                }
            }

            #endregion
        }

        #endregion

        #region Private Variables

        private DataTable _Table;
        private Field _RowFields, _ColFields, _TotalFields;
        private PlaceOfTotalType _PlaceOfTotal = PlaceOfTotalType.Right;

        #endregion

        #region Properties

        public DataTable DataSource
        {
            get { return _Table; }
            set { _Table = value; }
        }

        public Field Rows
        {
            get { return _RowFields; }
            set { _RowFields = value; }
        }
        public Field Columns
        {
            get { return _ColFields; }
            set { _ColFields = value; }
        }
        public Field Fields
        {
            get { return _TotalFields; }
            set { _TotalFields = value; }
        }

        public PlaceOfTotalType PlaceOfTotal
        {
            get { return _PlaceOfTotal; }
            set { _PlaceOfTotal = value; }
        }

        #endregion

        #region Constractor

        public Pivot()
        {
            _RowFields = new Field();
            _ColFields = new Field();
            _TotalFields = new Field();
        }

        #endregion

        #region Pivot Functions

        public DataTable ConvertToPivot()
        {
            if (_Table == null) return null;

            DataTable dt = CreatePivotTable(_Table, _RowFields, _ColFields, _TotalFields, _PlaceOfTotal);

            return dt;
        }

        public DataTable ConvertToPivot(DataTable SourceTable)
        {
            if (SourceTable == null) return null;

            _Table = SourceTable;
            DataTable dt = CreatePivotTable(_Table, _RowFields, _ColFields, _TotalFields, _PlaceOfTotal);

            return dt;
        }

        public DataTable CreateDistinctTable(DataTable SourceTable, int ColStartIndex, int ColEndIndex)
        {
            if (SourceTable == null) return null;

            DataTable dt = new DataTable(SourceTable.TableName);

            StringBuilder sb = new StringBuilder();
            for (int i = ColStartIndex; i <= ColEndIndex; i++)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(SourceTable.Columns[i].ColumnName);

                dt.Columns.Add(SourceTable.Columns[i].ColumnName);
            }

            DataView dv = new DataView(SourceTable, "", sb.ToString(), DataViewRowState.CurrentRows);

            if (dv.Count <= 0) return dt;

            DataRow lastrow = null;
            DataRow destrow = null;
            bool samerow;
            int rowcount = 0;
            foreach (DataRowView rowview in dv)
            {
                DataRow row = rowview.Row;
                samerow = false;
                if (lastrow != null)
                {
                    samerow = EqualRow(row, lastrow, ColStartIndex, ColEndIndex);
                    if (!samerow)
                    {
                        dt.Rows.Add(destrow);
                    }
                }
                if (!samerow)
                    destrow = dt.NewRow();
                rowcount++;

                for (int i = ColStartIndex; i <= ColEndIndex; i++)
                {
                    destrow[i - ColStartIndex] = row[i];
                }

                lastrow = row;
            }
            if (destrow != null) dt.Rows.Add(destrow);
            return dt;
        }
        public DataTable CreateGroupByTable(DataTable SourceTable, string NewTableName, Field RowFields, Field ColumnFields, Field TotalFields)
        {
            FieldInfo[] Fields = new FieldInfo[RowFields.Fields.Count + ColumnFields.Fields.Count + TotalFields.Fields.Count];
            RowFields.Fields.CopyTo(Fields, 0);
            ColumnFields.Fields.CopyTo(Fields, RowFields.Fields.Count);
            TotalFields.Fields.CopyTo(Fields, ColumnFields.Fields.Count);

            if (SourceTable == null || Fields == null) return null;

            DataTable dt = new DataTable(NewTableName);
            string groupby = "";

            foreach (FieldInfo field in Fields)
            {
                DataColumn dc = SourceTable.Columns[field.FieldName];
                if (field.Aggregate == AggregateType.None)
                {
                    dt.Columns.Add(field.FieldName, dc.DataType, dc.Expression);
                    if (groupby.Length > 0) groupby += ",";
                    groupby += field.FieldName;
                }
                else
                {
                    dt.Columns.Add(field.FieldName, dc.DataType);
                }
            }

            DataView dv = new DataView(SourceTable, "", groupby, DataViewRowState.OriginalRows);

            if (dv.Count <= 0) return dt;

            DataRow lastrow = null;
            DataRow destrow = null;
            bool samerow;
            int rowcount = 0;
            foreach (DataRowView rowview in dv)
            {
                DataRow row = rowview.Row;
                samerow = false;
                if (lastrow != null)
                {
                    samerow = EqualRow(row, lastrow, Fields);
                    if (!samerow)
                        dt.Rows.Add(destrow);
                }
                if (!samerow)
                    destrow = dt.NewRow();
                rowcount++;
                foreach (FieldInfo field in Fields)
                {
                    switch (field.Aggregate)
                    {
                        case AggregateType.None:
                        case AggregateType.Last:
                            destrow[field.FieldAlias] = row[field.FieldName];
                            break;
                        case AggregateType.First:
                            if (rowcount == 1) destrow[field.FieldAlias] = row[field.FieldName];
                            break;
                        case AggregateType.Count:
                            destrow[field.FieldAlias] = rowcount;
                            break;
                        case AggregateType.Sum:
                            destrow[field.FieldAlias] = Add(destrow[field.FieldAlias], row[field.FieldName]);
                            break;
                        case AggregateType.Min:
                            destrow[field.FieldAlias] = Min(destrow[field.FieldAlias], row[field.FieldName]);
                            break;
                        case AggregateType.Max:
                            destrow[field.FieldAlias] = Max(destrow[field.FieldAlias], row[field.FieldName]);
                            break;
                        case AggregateType.Avg:
                            destrow[field.FieldAlias] = Avg(destrow[field.FieldAlias], row[field.FieldName], rowcount);
                            break;
                    }
                }
                lastrow = row;
            }
            if (destrow != null) dt.Rows.Add(destrow);
            return dt;
        }
        public DataTable CreateGroupByCubeTable(DataTable SourceTable, Field RowFields, Field ColumnFields, Field TotalFields, PlaceOfTotalType PlaceOfTotal)
        {
            int FColIndex = RowFields.Fields.Count - 1;
            FieldInfo[] Fields = new FieldInfo[RowFields.Fields.Count + ColumnFields.Fields.Count + TotalFields.Fields.Count];
            RowFields.Fields.CopyTo(Fields, 0);
            ColumnFields.Fields.CopyTo(Fields, RowFields.Fields.Count);
            TotalFields.Fields.CopyTo(Fields, RowFields.Fields.Count + ColumnFields.Fields.Count);

            if (SourceTable == null || Fields == null) return null;

            DataTable dt = new DataTable(SourceTable.TableName);
            string groupby = "";
            int GFieldCnt = 0;
            FieldInfo[] GFields = new FieldInfo[Fields.Length];

            // Шинээр үүсгэж байгаа тэйбэлдээ багана үүсгэхдээ шинэ нэртэйгээр үүсгэнэ
            // Учир нь Aggregate баганын алиас нэрийг давхардуулж өгч болох учир алдаа 
            // гарах магадлалтай байгаа юм.
            int fieldcount = 0;
            foreach (FieldInfo field in Fields)
            {
                // баганын шинэ нэрийг энд хадгална
                fieldcount++;
                field.NewFieldName = fieldcount.ToString();

                DataColumn dc = SourceTable.Columns[field.FieldName];
                dt.Columns.Add(field.NewFieldName, dc.DataType, dc.Expression);

                if (field.Aggregate== AggregateType.None)
                {
                    if (groupby.Length > 0) groupby += ",";
                    groupby += field.FieldName;
                    GFields[GFieldCnt] = field;
                    GFieldCnt++;
                }
            }

            DataView dv = new DataView(SourceTable, "", groupby, DataViewRowState.OriginalRows);

            if (dv.Count <= 0) return dt;

            DataRow lastrow = null;
            bool samerow;
            int rowcount = 0;
            DataRow[] temprow;
            temprow = new DataRow[GFieldCnt];
            dv.AddNew();
            foreach (DataRowView rowview in dv)
            {
                DataRow row = rowview.Row;
                samerow = false;
                rowcount++;
                for (int i = GFieldCnt - 1; i >= FColIndex; i--)
                {
                    lastrow = temprow[i];
                    if (lastrow != null)
                    {
                        FieldInfo[] F = new FieldInfo[i + 1];
                        Array.Copy(GFields, F, F.Length);
                        samerow = EqualRow(row, lastrow, F);
                        if (!samerow && temprow[i][GFields[0].NewFieldName].ToString().Length != 0)
                        {
                            //for (int j = i + 1; j < GFieldCnt; j++)
                            //    temprow[i][GFields[j].NewFieldName] = (PlaceOfTotal == PlaceOfTotalType.Left ? DBNull.Value : "\xff");

                            dt.Rows.Add(temprow[i]);
                            rowcount = 1;
                            temprow[i] = dt.NewRow();
                        }
                    }
                    else
                    {
                        temprow[i] = dt.NewRow();
                    }

                    foreach (FieldInfo field in Fields)
                    {
                        switch (field.Aggregate)
                        {
                            case AggregateType.None:
                            case AggregateType.Last:
                                temprow[i][field.NewFieldName] = row[field.FieldName];
                                break;
                            case AggregateType.First:
                                if (rowcount == 1) temprow[i][field.NewFieldName] = row[field.FieldName];
                                break;
                            case AggregateType.Count:
                                if (temprow[i][field.NewFieldName].ToString().Length == 0) temprow[i][field.NewFieldName] = rowcount;
                                else temprow[i][field.NewFieldName] = Convert.ToInt16(temprow[i][field.NewFieldName].ToString()) + 1;
                                break;
                            case AggregateType.Sum:
                                temprow[i][field.NewFieldName] = Add(temprow[i][field.NewFieldName], row[field.FieldName]);
                                break;
                            case AggregateType.Min:
                                temprow[i][field.NewFieldName] = Min(temprow[i][field.NewFieldName], row[field.FieldName]);
                                break;
                            case AggregateType.Max:
                                temprow[i][field.NewFieldName] = Max(temprow[i][field.NewFieldName], row[field.FieldName]);
                                break;
                            case AggregateType.Avg:
                                temprow[i][field.NewFieldName] = Avg(temprow[i][field.NewFieldName], row[field.FieldName], rowcount);
                                break;
                        }
                    }
                }
            }
            return dt;
        }

        public DataTable CreatePivotTable(DataTable SourceTable, Field RowFields, Field ColumnFields, Field TotalFields, PlaceOfTotalType PlaceOfTotal)
        {
            DataTable dt = CreateGroupByCubeTable(SourceTable, RowFields, ColumnFields, TotalFields, PlaceOfTotal);
            return CreatePivotTableByCol(dt, RowFields, ColumnFields, TotalFields);
        }
        private DataTable CreatePivotTableByCol(DataTable SourceTable, Field RowFields, Field ColFields, Field TotalFields)
        {
            DataTable dt = new DataTable(SourceTable.TableName);
            int colcount = 0;

            // Шинээр багана үүсгэхдээ шууд дугаараар нэр өгч үүсгэнэ

            // Эхлээд мөрний талбарын тоогоор багана үүсгэнэ
            foreach (FieldInfo f in RowFields.Fields)
            {
                dt.Columns.Add(colcount.ToString());
                colcount++;
            }

            // Байж болох бүх багануудыг тодорхойлох
            // Ингэж урьдчилан баганаа үүсгэхгүй бол баганы дэд дүнгүүдийн байрлал
            // нь зөрөөд байгаа юм.
            int colstartindex = RowFields.Fields.Count;
            int colendindex = colstartindex + ColFields.Fields.Count - 1;

            // Баганын утгууд байрлах мөрүүдийг үүсгэнэ
            for (int c = 0; c <= ColFields.Fields.Count; c++)
            {
                DataRow newrow = dt.NewRow();
                for (int r = 0; r < RowFields.Fields.Count; r++)
                {
                    newrow[r] = ((FieldInfo)RowFields.Fields[r]).FieldAlias;
                }
                dt.Rows.Add(newrow);
            }

            // Баганаар заагдсан утгуудыг Distinct хийж аваад үүгээр багана үүсгэнэ
            // Энд Total утгууд нь DBNull утгаар орж ирж байгаад эрэмбэлэхээр хамгийн
            // эхэнд гарч ирээд байгаа. Тиймээс Total баганууд үүсэхдээ дандаа эхэнд нь
            // гарч ирэх болно. Хэрэв Total багануудыг баруун талд гаргана гэвэл
            // гэвэл GroupCube хийхдээ DBNull утгаар нэмэхгүй xFF гэсэн утгатайгаар
            // нэмэх хэрэгтэй.
            DataTable dtcol = CreateDistinctTable(SourceTable, colstartindex, colendindex);
            Hashtable hcol = new Hashtable();

            foreach (DataRow row in dtcol.Rows)
            {
                StringBuilder hashkey = new StringBuilder();

                // TotalFields ийн тоогоор багана үүсгэнэ
                for (int field = 0; field < TotalFields.Fields.Count; field++)
                {
                    DataColumn newcol = dt.Columns.Add(colcount.ToString());
                    for (int i = 0; i < dtcol.Columns.Count; i++)
                    {
                        hashkey.Append(row[i].GetHashCode());

                        if (row[i] == DBNull.Value || Convert.ToString(row[i]) == "\xff")
                        {// Total
                            dt.Rows[i][colcount] = ((FieldInfo)ColFields.Fields[i]).FieldAlias;
                        }
                        else
                            dt.Rows[i][colcount] = row[i];
                    }
                    FieldInfo fi = (FieldInfo)TotalFields.Fields[field];
                    dt.Rows[dtcol.Columns.Count][colcount] = fi.FieldAlias;

                    hashkey.Append(field.GetHashCode());

                    // Нэг баганад харгалзах бүх утгуудыг цуглуулж hash key үүсгэж хийнэ.
                    // Дараа нь эндээс хайж баганын дугаараа олно.
                    hcol.Add(hashkey.ToString(), colcount);

                    colcount++;
                }
            }

            DataRow lastrow = null;
            DataRow currentrow = null;
            int rowendindex = RowFields.Fields.Count - 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                // Хэрэв мөрийн утга солигдвол шинээр мөр орж ирж байгаа
                // тул шинээр мөр нэмнэ
                if (!EqualRow(row, lastrow, 0, rowendindex))
                {
                    currentrow = dt.NewRow();
                    for (int i = 0; i <= rowendindex; i++)
                    {
                        currentrow[i] = row[i];
                    }
                    dt.Rows.Add(currentrow);
                }

                StringBuilder hashkey = new StringBuilder();

                for (int i = colstartindex; i <= colendindex; i++)
                {
                    hashkey.Append(row[i].GetHashCode());
                }
                hashkey.Append(((int)0).GetHashCode());

                object colpos = hcol[hashkey.ToString()];
                if (colpos != null)
                {
                    for (int f = 0; f < TotalFields.Fields.Count; f++)
                    {
                        currentrow[Convert.ToInt32(colpos) + f] = row[colendindex + f + 1];
                    }
                }
                lastrow = row;
            }

            return dt;

            #region Batdelger Version

            //DataTable dt = new DataTable(SourceTable.TableName);
            //DataTable dt1 = new DataTable();
            //dt.Clear();
            //dt1.Clear();
            //DataRow NewRow;
            //ArrayList AddedColumns = new ArrayList();

            //int fieldcount = 0;
            //string CAlias = "";

            //foreach (FieldInfo f in RowFields.Fields)
            //{
            //    fieldcount++;
            //    f.NewFieldName = fieldcount.ToString();
            //    dt.Columns.Add(f.NewFieldName);
            //}

            //foreach (FieldInfo f in ColFields.Fields)
            //{
            //    fieldcount++;
            //    f.NewFieldName = fieldcount.ToString();
            //    CAlias += "-" + ",";
            //}

            //AddedColumns.Add(CAlias);

            //if (PlaceOfTotal == PlaceType.Left)
            //{
            //    foreach (FieldInfo f in TotalFields.Fields)
            //        dt.Columns.Add(CAlias + "_" + f.FieldAlias);
            //}
            //else
            //{
            //    foreach (FieldInfo f in TotalFields.Fields)
            //        dt1.Columns.Add(CAlias + "_" + f.FieldAlias);
            //}

            //NewRow = dt.NewRow();

            //foreach (FieldInfo f in RowFields.Fields) 
            //    NewRow[f.NewFieldName ] = SourceTable.Rows[0][f.NewFieldName];

            //dt.Rows.Add(NewRow);


            //string groupby = "";
            //string[] FieldNames = new String[ColFields.Fields.Count];
            //int i = 0;
            //foreach (FieldInfo field in ColFields.Fields)
            //{
            //    if (groupby.Length > 0) groupby += ",";
            //    groupby += field.FieldName;
            //    FieldNames[i] = field.FieldName;
            //    i++;
            //}

            //foreach (DataRow row in SourceTable.Rows)
            //{
            //    string ColAlias = "";

            //    foreach (FieldInfo f in ColFields.Fields)
            //    {
            //        if (string.IsNullOrEmpty(row[f.FieldAlias].ToString())) 
            //            ColAlias += "-" + ",";
            //        else
            //            ColAlias += row[f.FieldAlias].ToString() + ",";
            //    }

            //    if (AddedColumns.Contains(ColAlias))
            //    {
            //        if (ColAlias.Equals(CAlias) && PlaceOfTotal == PlaceType.Right)
            //        {
            //            DataRow nrow;
            //            nrow = dt1.NewRow();
            //            foreach (FieldInfo f in TotalFields.Fields) nrow[ColAlias + "_" + f.FieldAlias] = row[f.FieldAlias];
            //            dt1.Rows.Add(nrow);
            //        }
            //        else
            //        {
            //            bool samerow = false;
            //            foreach (FieldInfo f in RowFields.Fields)
            //                if (EqualRowByOneField(dt.Rows[dt.Rows.Count - 1], row, f)) samerow = true;
            //                else samerow = false;
            //            if (samerow)
            //            {
            //                foreach (FieldInfo f in TotalFields.Fields) dt.Rows[dt.Rows.Count - 1][ColAlias + "_" + f.FieldAlias] = row[f.FieldAlias];
            //            }
            //            else
            //            {
            //                NewRow = dt.NewRow();
            //                foreach (FieldInfo f in RowFields.Fields) NewRow[f.FieldAlias] = row[f.FieldAlias];
            //                foreach (FieldInfo f in TotalFields.Fields) NewRow[ColAlias + "_" + f.FieldAlias] = row[f.FieldAlias];
            //                dt.Rows.Add(NewRow);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        AddedColumns.Add(ColAlias);
            //        foreach (FieldInfo f in TotalFields.Fields)
            //        {
            //            dt.Columns.Add(ColAlias + "_" + f.FieldAlias);
            //            dt.Rows[dt.Rows.Count - 1][ColAlias + "_" + f.FieldAlias] = row[f.FieldAlias];
            //        }
            //    }
            //}
            //if (PlaceOfTotal == PlaceType.Right)
            //{
            //    foreach (DataColumn Col in dt1.Columns)
            //        dt.Columns.Add(Col.ColumnName);
            //    i = 0;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        foreach (DataColumn Col in dt1.Columns)
            //            row[Col.ColumnName] = dt1.Rows[i][Col.ColumnName];
            //        i++;
            //    }
            //}

            //dt = CreateHeadRows(dt, ColFields);

            //return dt;

            #endregion

        }

        #endregion

        #region Main Functions

        private bool EqualRow(DataRow row1, DataRow row2, int startindex, int endindex)
        {
            if (row1 == null || row2 == null) return false;

            for (int i = startindex; i <= endindex; i++)
            {
                if (!row1[i].Equals(row2[i]))
                    return false;
            }
            return true;
        }
        private bool EqualRow(DataRow SourceRow, DataRow DestRow, FieldInfo[] Fields)
        {
            foreach (FieldInfo field in Fields)
            {
                if (!SourceRow[field.FieldName].Equals(DestRow[field.NewFieldName]))
                    return false;
            }
            return true;
        }
        private bool EqualRow(DataRow SourceRow, DataRow DestRow, ArrayList Fields)
        {
            foreach (FieldInfo field in Fields)
            {
                if (!SourceRow[field.FieldName].Equals(DestRow[field.NewFieldName]))
                    return false;
            }
            return true;
        }
        private bool EqualRowByOneField(DataRow SourceRow, DataRow DestRow, FieldInfo Field)
        {
            if (Field.Aggregate != null) return false;
            if (!SourceRow[Field.FieldName].Equals(DestRow[Field.FieldName]))
                return false;
            return true;
        }
        private bool Equal(object a, object b)
        {
            if ((a is DBNull) && (b is DBNull))
                return true;    //both are null
            if ((a is DBNull) || (b is DBNull))
                return false;    //only one is null
            return (a == b);    //value type standard comparison
        }

        private object Min(object a, object b)
        {
            //Returns MIN of two values - DBNull is less than all others
            if ((a is DBNull) || (b is DBNull)) return DBNull.Value;
            if (((IComparable)a).CompareTo(b) == -1) return a;
            else return b;
        }
        private object Max(object a, object b)
        {
            //Returns Max of two values - DBNull is less than all others
            if (a is DBNull) return b;
            if (b is DBNull) return a;
            if (((IComparable)a).CompareTo(b) == 1) return a;
            else return b;
        }
        private object Add(object a, object b)
        {
            //Adds two values - if one is DBNull, then returns the other
            if (a is DBNull) return b;
            if (b is DBNull) return a;
            return (Convert.ToDecimal(a) + Convert.ToDecimal(b));
        }
        private object Avg(object avg, object a, int rowcount)
        {
            decimal d1 = Convert.ToDecimal(avg is DBNull ? 0 : avg);
            decimal d2 = Convert.ToDecimal(a is DBNull ? 0 : a);
            return (d1 * (rowcount - 1) / rowcount + d2 / rowcount);
        }

        #endregion
    }
}
