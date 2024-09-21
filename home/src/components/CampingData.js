import React, { Component } from 'react';
import "./CampingDataStyle.css";

class CampingData extends Component {
  render() {
    return (
      
  

            <div className={this.props.className}>
             <div className="camp-text">
             <h2 >{this.props.heading}</h2>
                <p>
               {this.props.text}
</p>
             </div>
<div className='imgcamping'>
<img alt="img" src={this.props.img1}></img>

<img alt="img" src={this.props.img2}></img>
</div>
            </div>

      
    );
  }
}

export default CampingData;
