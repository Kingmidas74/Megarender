import React, { Component } from 'react';
import SwaggerUI from 'swagger-ui-react';
import Config from './organization_config.json';
import Sidebar from './Sidebar.js'

class App extends Component {
  constructor(props) {
    super(props);
    let services = [{
      name:'identity',
      url:'http://localhost:8000/api/management/swagger/0.1/swagger.json'
    },{
      name:'management',
      url:'http://localhost:5002/swagger/0.1/swagger.json'
    },{
      name:'storage',
      url:'http://localhost:5004/swagger/0.1/swagger.json'
    }];
    this.state = {
        organizationConfig: null,
        definitionList: services,
        definitionLink: services[0].url
      }
      
      this.updateDefinitionLink = this.updateDefinitionLink.bind(this)
    }

  componentWillMount() {
    this.setState({
      organizationConfig:  Config.orgData,
    })
  }

  updateDefinitionLink(apiurl) {
    this.setState({
      definitionLink: apiurl
    })
  }

  render() {
    return (
      <div className="App">
        <Sidebar 
          organizationConfig={this.state.organizationConfig}
          definitionList={this.state.definitionList}
          updateDefinitionLink={this.updateDefinitionLink}
        />
        
        <div id="api-data">
          <SwaggerUI 
            url={this.state.definitionLink}
            docExpansion="list"
          />
        </div>
      </div>
    );
  }
}

export default App;
