#import <UIKit/UIKit.h>

extern "C"
{
    void _ShowIOSAlert(const char* message)
    {
        if (message == NULL)
        {
            return;
        }

        dispatch_async(dispatch_get_main_queue(), ^{
            NSString* msg = [NSString stringWithUTF8String:message];

            UIAlertController* alert =
                [UIAlertController alertControllerWithTitle:@"CleverTap"
                                                    message:msg
                                             preferredStyle:UIAlertControllerStyleAlert];

            UIAlertAction* okAction =
                [UIAlertAction actionWithTitle:@"OK"
                                         style:UIAlertActionStyleDefault
                                       handler:nil];

            [alert addAction:okAction];

            UIViewController* rootViewController = nil;

            for (UIWindowScene* scene in UIApplication.sharedApplication.connectedScenes)
            {
                if (scene.activationState == UISceneActivationStateForegroundActive)
                {
                    rootViewController = scene.windows.firstObject.rootViewController;
                    break;
                }
            }

            if (rootViewController != nil)
            {
                [rootViewController presentViewController:alert animated:YES completion:nil];
            }
        });
    }
}
