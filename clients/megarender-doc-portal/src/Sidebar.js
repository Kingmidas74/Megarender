import React from 'react';
import APILink from './APILink.js'

const Sidebar = props => {
    let organizationConfig = props.organizationConfig
    let apiLinks = props.definitionList.map(l=> {        
        return <APILink 
                    apiLinkData={l}
                    updateDefinitionLink={props.updateDefinitionLink}
                />
    });

  return (
    <div className="side-bar">
        <div className="side-bar-header">            
            <h1>{organizationConfig.displayName}</h1>            
        </div>
        <div className="side-bar-body">
            <h3>Services</h3>
            {apiLinks}
        </div>
    </div>
  )
}

export default Sidebar;