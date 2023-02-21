import "./SignUp.css";
import { TextField } from "@fluentui/react/lib/TextField";

const SignUp = () => {
    const handleErrorMessage = (value: string) => {
        if (value.length <= 8) {
            return "Length must be at least 8 characters long"
        }
    }

    return (
        <div className="sign-up">
            <form>
                <TextField
                    label="UserName"
                    onGetErrorMessage={handleErrorMessage}
                    validateOnLoad={false}
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Email"
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Password"
                    validateOnFocusOut
                    required
                />

                <TextField
                    label="Confirm Password"
                    validateOnFocusOut
                    required
                />

                <button>Sign Up</button>
            </form>
        </div>
    )
}

export default SignUp;