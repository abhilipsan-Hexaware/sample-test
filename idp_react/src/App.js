import React, { useState }  from "react";
import FileUpload from "./components/file-upload.component";
import "bootstrap/dist/css/bootstrap.min.css";
import UploadFiles from "./components/file-upload.component";

function App() {
//   const [newUserInfo, setNewUserInfo] = useState({
//     profileImages: []
//   });
// console.log("newUserInfo", newUserInfo);
//   const updateUploadedFiles = (files) =>
//   setNewUserInfo({ ...newUserInfo, profileImages: files });
  
//   const handleSubmit = (event) => {
//     event.preventDefault();
//     //logic to create a new user...
//   };
//   return (
//     <div className="App">
//       <form onSubmit={handleSubmit}>
//       <FileUpload
//           accept=".jpg,.png,.jpeg,.pdf"
//           // label="Profile Image(s)"
//           multiple
//           updateFilesCb={updateUploadedFiles}
//         />
//         {/* <button type="submit">Create New User</button> */}
//       </form>
//     </div>
//   );
return (
  <div className="container" style={{ width: "600px" }}>
    <div style={{ margin: "20px" }}>
   
      <h4> upload Files</h4>
    </div>

    <UploadFiles />
  </div>
);

}

export default App;
