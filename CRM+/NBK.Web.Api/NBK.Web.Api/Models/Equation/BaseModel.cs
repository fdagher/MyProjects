using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace NBK.Web.Api.Models.Equation
{
    public abstract class BaseModel
    {

        /// <summary>
        /// Build request message 
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            MessageDefinition messageDefinition = null;
            Position position = null;
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr is MessageDefinition)
                {
                    messageDefinition = (MessageDefinition)attr;
                }
            }

            string message = new string(' ', messageDefinition.Length);
            StringBuilder sb = new StringBuilder(message);

            PropertyInfo[] propertyInfo = this.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public);


            for (int i = 0; i < propertyInfo.Length; i++)
            {
                position = null;
               
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo[i]))
                {
                    if (attr is Position)
                    {
                        position = (Position)attr;
                    }

                }

                if (position != null)
                {
                    object value = propertyInfo[i].GetValue(this, null);
                    if (value != null && value.ToString() != string.Empty)
                    {
                        sb.Remove(position.StartPos, position.Length);
                        sb.Insert(position.StartPos, value.ToString().PadRight(position.Length));
                    }
                }

              
            }

            return sb.ToString();
        }


        /// <summary>
        /// Build Response class from response string
        /// </summary>
        /// <param name="responseMessage"></param>
        public virtual void DeSerialize(string responseMessage)
        {
            Position position = null;
            RepeatData repeatData = null;
            RepeatCount repeatCount = null;
            PropertyInfo[] propertyInfo = this.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public);


            for (int i = 0; i < propertyInfo.Length; i++)
            {
                position = null;
                repeatData = null;
                repeatCount = null;

                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo[i]))
                {
                    if (attr is Position)
                    {
                        position = (Position)attr;
                    }

                    if (attr is RepeatData)
                    {
                        repeatData = (RepeatData)attr;
                    }

                    if (attr is RepeatCount)
                    {
                        repeatCount = (RepeatCount)attr;
                    }
                }

                if (position != null)
                {
                    try
                    {
                        string fieldValue = responseMessage.Substring(position.StartPos, position.Length).Trim();
                        string fieldName = propertyInfo[i].Name;
                        string fieldType = propertyInfo[i].PropertyType.FullName;
                        object o = Convert.ChangeType(fieldValue, Type.GetType(fieldType));
                        propertyInfo[i].SetValue(this, o, null);
                    }
                    catch
                    {
                        
                    }
                 
                }

                //Populate repeating data
                if (repeatData != null && repeatCount != null)
                {
                    Type type = propertyInfo[i].PropertyType;
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var propertyInstance = Activator.CreateInstance(type);
                        Type itemType = type.GetGenericArguments()[0];
                        if (itemType != null)
                        {
                            //Get number of repeated records in the message
                            int recordCount = 0;
                            try
                            {
                                recordCount = int.Parse(responseMessage.Substring(repeatCount.StartPos, repeatCount.Length).Trim());
                            }
                            catch { }

                            for (int count = 0; count < recordCount; count++)
                            {
                                var itemInstance = Activator.CreateInstance(itemType);
                                MethodInfo methodInfo = itemType.GetMethod("DeSerialize");
                                if (methodInfo != null)
                                {
                                    object[] parameters = new object[1];
                                    parameters[0] = responseMessage.Substring(repeatData.StartPos + (count * repeatData.Length), repeatData.Length);
                                    methodInfo.Invoke(itemInstance, parameters);
                                }

                                type.GetMethod("Add").Invoke(propertyInstance, new[] { itemInstance });
                            }
                            propertyInfo[i].SetValue(this, propertyInstance, null);

                        }
                    
                    }

                    
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            XmlTextWriter xmlWriter = new XmlTextWriter(new StringWriter(sb));
            this.ToXml(xmlWriter, true);
            return sb.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="complete"></param>
        private void ToXml(XmlTextWriter xmlWriter, bool complete)
        {
            xmlWriter.WriteStartElement(this.GetType().Name);
            PropertyInfo[] propertyInfo = this.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < propertyInfo.Length; i++)
            {
                bool ignore = false;
                foreach (Attribute attribute in Attribute.GetCustomAttributes(propertyInfo[i]))
                {
                    if (attribute.GetType() == typeof(XmlIgnoreAttribute))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (!ignore)
                {
                    string name = propertyInfo[i].Name;
                    string type = propertyInfo[i].PropertyType.Name;
                    object value = propertyInfo[i].GetValue(this, null);
                    if (type == "DateTime")
                    {
                        xmlWriter.WriteElementString(name, value == null ? "" : ((DateTime)value).ToString("g"));
                    }
                    else
                    {
                        xmlWriter.WriteElementString(name, value == null ? "" : value.ToString());
                    }

                }
            }
            if (complete)
            {
                xmlWriter.WriteEndElement();
            }
        }
    }
}
