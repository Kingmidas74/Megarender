
import React from 'react';

const APILink = props => {
    let name = props.apiLinkData.name
    let apiLink = `${window.__env.API_URI}${props.apiLinkData.path}`

    function handleClick() {
      props.updateDefinitionLink(apiLink)
    }

  return (  
    <div className="api-link" onClick={() => handleClick()}>
      {name}
    </div>
  )
}

export default APILink;