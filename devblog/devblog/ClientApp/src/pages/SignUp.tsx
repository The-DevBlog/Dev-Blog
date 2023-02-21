import "./SignUp.css";

const SignUp = () => {
    return (
        <div className="sign-up">
            <form>
                <label>UserName</label>
                <input
                    type="text"
                    required
                />

                <label>Email</label>
                <input
                    type="email"
                    required
                />

                <label>Password</label>
                <input
                    type="password"
                    required
                />

                <label>Confirm Password</label>
                <input
                    type="password"
                    required
                />

                <button>Sign Up</button>
            </form>
        </div>
    )
}

export default SignUp;