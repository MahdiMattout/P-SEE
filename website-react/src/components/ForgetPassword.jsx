import React, { useState } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import "./Login.css"
import { AiOutlineMail } from 'react-icons/ai'

export default function ForgetPassword() {
  const [credentialUsername, setUsername] = useState("");
  const [result, setResult] = useState()
  const submit = async (e) => {
    e.preventDefault()
    const params = decodeURIComponent(new URLSearchParams({
      credentialUsername,
    }));
    const response = await fetch(
      `http://pc-health.somee.com/Admin/ForgetPasswordUsername?${params}`,
      {
        method: "POST",
      }
    )
    const ans = await response.json()
    setResult(ans)
  };
  if (result) {
    localStorage.setItem("Email", credentialUsername);
    return <Redirect to="/forgotpassword/Code" />
  }
  return (
    <div className="div_design">
      <form onSubmit={submit} className="forgot_pass_form_container">
        {/* <p className="forgot_pass">Enter your email:</p> */}
        <h2 className="h1_d">Reset Password</h2>

        <p className="forgot_pass_message">
          Verify your identity using your Email Address
        </p>

        <div className="input-icon">
          <AiOutlineMail className="icon" />
          <input
            type="email"
            className="design_input"
            placeholder="Email"
            required
            onChange={(e) => setUsername(e.target.value)}>
          </input>
        </div>

        <button className="login_button" type="submit">Submit</button>

        <div className="navigations_login">
          {/* <div className="plinks"> */}
            <Link to="/" className="links">
              cancel
            </Link>
          {/* </div> */}
        </div>

      </form>
    </div>
  );
}