/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//-CRE-


using System;
using Glass.Mapper.Pipelines.DataMapperResolver;
using Glass.Mapper.Umb.Configuration;
using umbraco.BusinessLogic;

namespace Glass.Mapper.Umb.DataMappers
{
    /// <summary>
    /// UmbracoInfoMapper
    /// </summary>
    public class UmbracoInfoMapper : AbstractDataMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoInfoMapper"/> class.
        /// </summary>
        public UmbracoInfoMapper()
        {
            ReadOnly = true;
        }

        /// <summary>
        /// Maps data from the .Net property value to the CMS value
        /// </summary>
        /// <param name="mappingContext"></param>
        /// <exception cref="MapperException">You can not set an empty or null Item name</exception>
        /// <exception cref="System.NotSupportedException">
        /// Can't set Name. Value is not of type System.String
        /// or
        /// You can not save UmbracoInfo {0}.Formatted(config.Type)
        /// </exception>
        public override void MapToCms(AbstractDataMappingContext mappingContext)
        {
           throw new NotImplementedException();
        }

        /// <summary>
        /// Maps data from the CMS value to the .Net property value
        /// </summary>
        /// <param name="mappingContext"></param>
        /// <returns></returns>
        /// <exception cref="MapperException">UmbracoInfoType {0} not supported.Formatted(config.Type)</exception>
        public override object MapToProperty(AbstractDataMappingContext mappingContext)
        {
            var context = mappingContext as UmbracoDataMappingContext;
            var content = context.Content;
            var config = Configuration as UmbracoInfoConfiguration;

            switch (config.Type)
            {
                case UmbracoInfoType.Name:
                    return content.Name;
                case UmbracoInfoType.Path:
                    return content.Path;
                case UmbracoInfoType.ContentTypeAlias:
                    return content.ContentType.Alias;
                //case UmbracoInfoType.ContentTypeName: //TODO LC
                //    return content.ContentType.Alias;
                //case UmbracoInfoType.Url:
                //    return content.Name.FormatUrl().ToLower();
                case UmbracoInfoType.CreateDate:
                    return content.CreateDate;
                case UmbracoInfoType.UpdateDate:
                    return content.UpdateDate;
                case UmbracoInfoType.Version:
                    return content.Version;
                case UmbracoInfoType.Creator:
                    var user = new User(content.CreatorId);
                    return user.LoginName;
                default:
                    throw new MapperException("UmbracoInfoType {0} not supported".Formatted(config.Type));
            }
        }

        /// <summary>
        /// Sets up the data mapper for a particular property
        /// </summary>
        /// <param name="args"></param>
        public override void Setup(DataMapperResolverArgs args)
        {
            var config = args.PropertyConfiguration as UmbracoInfoConfiguration;
            this.ReadOnly = config.Type != UmbracoInfoType.Name;
            base.Setup(args);
        }

        /// <summary>
        /// Indicates that the data mapper will mapper to and from the property
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool CanHandle(Mapper.Configuration.AbstractPropertyConfiguration configuration, Context context)
        {
            return configuration is UmbracoInfoConfiguration;
        }
    }
}




