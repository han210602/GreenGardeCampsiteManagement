import "./EventStyle.css";
import React from 'react'; 

const EventData = (props) => {

      return (
        <div className="e-card">
        <div className="e-img">
<img alt="image" src={props.image}/>

        </div>
        <h4>{props.heading}</h4>
        <p>{props.text}</p>

        </div>
      );
    }
  

export default EventData;
