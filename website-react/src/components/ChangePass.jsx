import React, { useState } from 'react'
import { Redirect } from 'react-router-dom'
import "./RegisterStyle.css"
import { Link } from "react-router-dom";
import { AiOutlineMail } from 'react-icons/ai'
import { RiLockPasswordFill } from 'react-icons/ri'
import { ImKey } from 'react-icons/im'
import { FaUserAlt } from 'react-icons/fa'

function ChangePass() {
  const [CredentialUsername, setUsername] = useState("")
  const [OldPassword, setOldPassword] = useState("")
  const [NewPassword, setNew] = useState("")
  const [ConfirmedPassword, setConfirmed] = useState("")
  const [result, setResult] = useState()
  // const [error, setError] = useState("")
  const [validPass, setValidPass] = useState(false);

  const handlePassword = (e) => {
    e.preventDefault();
    var pass = e.target.value;
    var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([!@#\$%\^&\*]*)(?=.{8,})^/;
    var test = reg.test(pass);
    if (test === true) {
      setNew(pass);
    }
  };

  const submit = async (e) => {
    e.preventDefault();
    const response =
      await fetch(
        "http://pc-health.somee.com/Admin/ChangePassword", //You receive true or false, delete token when true and logout
        {
          method: "POST",
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${localStorage.getItem("token")}` },
          body: JSON.stringify({
            CredentialUsername,
            OldPassword,
            NewPassword,
          }),
        }
      );
    const res = await response.json()
    setResult(res)
  }
  // console.log(NewPassword === ConfirmedPassword)
  // const checkValidation = (e) => {
  //   setConfirmed(e.target.value)
  //   if (NewPassword !== ConfirmedPassword) {
  //     setError("Confirm password should match the new password"); setResult(false)
  //   } else { setError("") }
  // }

  if (result === true) {
    return <Redirect to='/table' />
  }

  if (result === false) {

    return (
      <div className="rdiv_design">
        <form className="rform_container" onSubmit={submit}>
          <h2 className="rh1_d">Change Password</h2>


          {/* {(result === false) ? <p className="rno_match">Email and old password do not match</p> : 
         <p className="rno_match">{error}</p>} */}


          {/* {(result === false) ? <p style={{ "color": "white" }}>Email and old password do not match</p> : ""}
        <p style={{ "color": "white" }}>{error}</p> */}

          {/* {(result === false) ? <p className="rno_match">Email and old password do not match</p> : ''}
          {(error === "") ? <><br /><br /></> : <p className="rno_match">{error}</p>} */}
          

          {NewPassword !== ConfirmedPassword ? (
            <p className="rfailed_register">Passwords do not match</p>
          ) : (
            <p className="rfailed_register">Email and password do not match</p>
          )}

          <div className="rdiv1">
            <div className="rinput-icon">
              <FaUserAlt className="ricon" />
              <input
                className="rdesign_input"
                type="text"
                placeholder="Email"
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="rinput-icon">
              <ImKey className="ricon" />
              <input
                className="rdesign_input"
                type="password"
                placeholder="Old Password"
                onChange={(e) => setOldPassword(e.target.value)}
              />
            </div>

          </div>


          <div className="rdiv2">
            <div className="rinput-icon">
              <input
                className="rno_icon_design_input"
                type="password"
                placeholder="New Password"
                onChange={(e) => handlePassword(e)}
              />
            </div>

            <div className="rinput-icon">
              <input
                className="rno_icon_design_input"
                type="password"
                placeholder="Confirm new password"
                onChange={(e) => setConfirmed(e.target.value)}
              />
            </div>
          </div>


          <button className="rlogin_button" type="submit" disabled={NewPassword !== ConfirmedPassword}>
            Submit
        </button>


          <hr />

          <div className="rplinks">
            <Link to="/table" className="rlinks">
              cancel
          </Link>
          </div>


        </form>

      </div>
    );
  }
  return (
    <div className="rdiv_design">
        <form className="rform_container" onSubmit={submit}>
          <h2 className="rh1_d">Change Password</h2>

          {NewPassword.length ===0  ? <p className="weak_password">Passwords should be at least 8 character long, have at least one uppercase and one lowercase character, and must include numbers</p> : 
          (
            (NewPassword !==ConfirmedPassword && NewPassword.length >0) ? <p className="rfailed_register">Passwords do not match</p> : <><br/><br/></>
          )
          }

          <div className="rdiv1">
            <div className="rinput-icon">
              <FaUserAlt className="ricon" />
              <input
                className="rdesign_input"
                type="text"
                placeholder="Email"
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="rinput-icon">
              <ImKey className="ricon" />
              <input
                className="rdesign_input"
                type="password"
                placeholder="Old Password"
                onChange={(e) => setOldPassword(e.target.value)}
              />
            </div>

          </div>


          <div className="rdiv2">
            <div className="rinput-icon">
              <input
                className="rno_icon_design_input"
                type="password"
                placeholder="New Password"
                onChange={(e) => handlePassword(e)}
              />
            </div>

            <div className="rinput-icon">
              <input
                className="rno_icon_design_input"
                type="password"
                placeholder="Confirm new password"
                onChange={(e) => setConfirmed(e.target.value)}
              />
            </div>
          </div>


          <button className="rlogin_button" type="submit" disabled={NewPassword!==ConfirmedPassword}>
            Submit
        </button>


          <hr />

          <div className="rplinks">
            <Link to="/table" className="rlinks">
              cancel
          </Link>
          </div>


        </form>

      </div>
  );
}

export default ChangePass